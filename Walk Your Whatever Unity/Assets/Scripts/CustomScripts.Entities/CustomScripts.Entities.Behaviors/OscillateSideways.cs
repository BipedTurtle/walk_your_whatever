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
        private int stepsPerHalfOscillation = 20;

        private int stepsUntilTurn = 10;
        private float amplitude = .2f;
        private float direction = 1f;
        public (IMovementBehavior behavior, Vector3 movementAdded) GetBehavior()
        {
            var unitMovement = Vector3.right;
            var speedDamping = Abs(Sin(Time.fixedTime));
            Debug.Log(speedDamping);
            var stepSize = this.amplitude * speedDamping;
            var movement = this.direction * unitMovement * stepSize;

            this.ChangeDirIfNecessary();

            return (behavior: this, movementAdded: movement);
        }


        private void ChangeDirIfNecessary()
        {
            this.stepsUntilTurn--;
            if (this.stepsUntilTurn <= 0) {
                this.direction *= -1f;
                this.stepsUntilTurn = this.stepsPerHalfOscillation;
            }
        }
    }
}
