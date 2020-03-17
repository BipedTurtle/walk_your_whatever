using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Mathf;

namespace CustomScripts.Entities.Behaviors
{
    public class OscillateSideways : IMovementBehavior
    {
        // increasing the number of steps increases the amplitude of a Whatever
        private int stepsPerHalfOscillation = 30;
        private int stepsUntilTurn;

        public OscillateSideways()
        {
            this.stepsUntilTurn = this.stepsPerHalfOscillation / 2;
        }

        private float stepSpeed = 5f;
        private float direction = 1f;
        public (IMovementBehavior behavior, Vector3 movementAdded) GetBehavior()
        {
            var unitMovement = Vector3.right;
            var stepSize = this.stepSpeed * Time.fixedDeltaTime;
            var movement = this.direction * unitMovement * stepSize;

            this.ChangeDirIfNecessary();

            return (behavior: this, movementAdded: movement);
        }


        private void ChangeDirIfNecessary()
        {
            this.stepsUntilTurn--;
            if (this.stepsUntilTurn <= 0)
            {
                this.direction *= -1f;
                this.stepsUntilTurn = this.stepsPerHalfOscillation;
            }
        }
    }
}
