using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Mathf;
using CustomScripts.Managers;

namespace CustomScripts.Entities.Behaviors
{
    public class OscillateSideways : IMovementBehavior
    {
        // increasing the number of steps increases the amplitude of a Whatever
        private int stepsPerHalfOscillation = 40;
        private int stepsUntilTurn;

        public OscillateSideways()
        {
            this.stepsUntilTurn = this.stepsPerHalfOscillation / 2;
            this.nextBehavior = this;
        }

        private IEnumerator UpdateBehavior(float after)
        {
            yield return new WaitForSeconds(after);
            this.nextBehavior = new FixedPointWalking();
        }

        private IMovementBehavior nextBehavior;
        private float stepSpeed = 4.5f;
        private float direction = 1f;
        public (IMovementBehavior behavior, Vector3 movementAdded) GetBehaviorAndMovement()
        {
            var unitMovement = Vector3.right;
            var stepSize = this.stepSpeed * Time.fixedDeltaTime;
            var movement = this.direction * unitMovement * stepSize;

            this.ChangeDirIfNecessary();
            this.ChangeBeahviorIfNecessary();

            return (behavior: this.nextBehavior, movementAdded: movement);
        }


        private int changeBehaviorThreshold = 20;
        private int oscillationCount;
        private void ChangeBeahviorIfNecessary()
        {
            if (this.oscillationCount >= this.changeBehaviorThreshold)
                this.nextBehavior = new FixedPointWalking();
        }


        private void ChangeDirIfNecessary()
        {
            this.stepsUntilTurn--;
            if (this.stepsUntilTurn <= 0) {
                this.direction *= -1f;
                this.stepsUntilTurn = this.stepsPerHalfOscillation;
                this.oscillationCount++;
            }
        }
    }
}
