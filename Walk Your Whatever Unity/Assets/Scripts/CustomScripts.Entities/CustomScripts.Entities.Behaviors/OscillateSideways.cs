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

        // stepsPerHalfOscillation is now obsolete. It's just here as a reference
        private int stepsPerHalfOscillation = 30;

        public OscillateSideways(Vector3 moveDirThisFrame)
        {
            this.distanceUntilTurn = this.StepSpeed * Time.fixedDeltaTime * 15;

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

            this.ChangeDirIfNecessary(movement.magnitude);

            var behavior = this.GetNextBehavior();

            return (behavior, movement);
        }


        private float direction = 1f;
        private float distanceUntilTurn;
        private void ChangeDirIfNecessary(float distanceTraveled)
        {
            this.distanceUntilTurn -= distanceTraveled;
            if (distanceUntilTurn < 0) {
                this.direction *= -1f;
                var curbFix = 1f - Movement.Curb;
                this.distanceUntilTurn = (this.StepSpeed / curbFix) * Time.fixedDeltaTime * 30;
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
