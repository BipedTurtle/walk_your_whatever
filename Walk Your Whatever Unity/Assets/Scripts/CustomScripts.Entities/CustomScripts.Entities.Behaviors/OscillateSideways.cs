using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Mathf;
using CustomScripts.Managers;
using CustomScripts.Fundamentals;
using CustomScripts.Environment;

namespace CustomScripts.Entities.Behaviors
{
    public class OscillateSideways : IMovementBehavior
    {
        public enum OscillationDirection { Left, Right }

        private int stepsPerHalfOscillation = 30;
        public OscillateSideways(Vector3 moveDirThisFrame)
        {
            this.distanceUntilTurn = this.StepSpeed * Time.fixedDeltaTime * (this.stepsPerHalfOscillation /2) * this.timeFix;

            var oscillationDirection = (moveDirThisFrame.x < 0) ? OscillationDirection.Left : OscillationDirection.Right;
            this.SetStartingDirection(oscillationDirection);
        }


        private void SetStartingDirection(OscillationDirection startDir)
        {
            switch (startDir) {
                case OscillationDirection.Left:
                    this.direction *= -1f;
                    break;
                case OscillationDirection.Right:
                    this.direction *= 1f;
                    break;
            }
        }


        private float StepSpeed => 4.5f * (1f - Movement.Curb);
        public (IMovementBehavior behavior, Vector3 movementAdded) GetBehaviorAndMovement()
        {
            var unitMovement = Vector3.right;
            var stepSize = this.StepSpeed * Time.fixedDeltaTime;
            var movement = this.direction * unitMovement * stepSize;

            #region Movement Analytics
            var oscillationPerFrame = movement.magnitude / this.regularOscillationAmount;
            MovementAnalytics.MeasureOscillation(oscillationPerFrame);
            #endregion

            this.ChangeDirIfNecessary(movement.magnitude);

            var behavior = this.GetNextBehavior();

            return (behavior, movement);
        }


        private float direction = 1f;
        private float distanceUntilTurn;
        private float curbFix => 1f - Movement.Curb;
        private float timeFix = .02f / .013333f;
        private float regularOscillationAmount => (this.StepSpeed / curbFix) * Time.fixedDeltaTime * this.stepsPerHalfOscillation * this.timeFix;
        private void ChangeDirIfNecessary(float distanceTraveled)
        {
            this.distanceUntilTurn -= distanceTraveled;
            if (distanceUntilTurn < 0) {
                this.direction *= -1f;
                this.distanceUntilTurn = regularOscillationAmount;
            }
        }

        private IMovementBehavior GetNextBehavior()
        {
            var attractorNearby = BigDog.GetClosest();
            //Debug.Log(attractorNearby);
            if (attractorNearby == null)
                return this;
            else
                return new WalkTowardOther();
        }
    }
}
