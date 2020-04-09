using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Mathf;
using CustomScripts.Managers;
using CustomScripts.Fundamentals;

namespace CustomScripts.Entities.Behaviors
{
    public class OscillateSideways : IMovementBehavior
    {
        // stepsPerHalfOscillation is now obsolete. It's just here as a reference
        private int stepsPerHalfOscillation = 30;

        public OscillateSideways()
        {
            this.distanceUntilTurn = this.StepSpeed * Time.fixedDeltaTime * 15;
            this.nextBehavior = this;
        }

        private IEnumerator UpdateBehavior(float after)
        {
            yield return new WaitForSeconds(after);
            this.nextBehavior = new FixedPointWalking();
        }

        private IMovementBehavior nextBehavior;
        private float StepSpeed => 4.5f * (1f - Movement.Curb);
        private float direction = 1f;
        public (IMovementBehavior behavior, Vector3 movementAdded) GetBehaviorAndMovement()
        {
            var unitMovement = Vector3.right;
            var stepSize = this.StepSpeed * Time.fixedDeltaTime;
            var movement = this.direction * unitMovement * stepSize;

            this.ChangeDirIfNecessary(movement.magnitude);
            //this.ChangeBeahviorIfNecessary();

            return (behavior: this.nextBehavior, movementAdded: movement);
        }

        #region This code might get omitted completely due to design decisions
        private int changeBehaviorThreshold = 20;
        private int oscillationCount;
        private void ChangeBeahviorIfNecessary()
        {
            if (this.oscillationCount >= this.changeBehaviorThreshold)
                this.nextBehavior = new FixedPointWalking();
        }
        #endregion

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
    }
}
