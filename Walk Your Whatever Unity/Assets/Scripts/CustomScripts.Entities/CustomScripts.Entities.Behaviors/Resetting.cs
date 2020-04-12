using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Fundamentals;
using CustomScripts.Environment;

namespace CustomScripts.Entities.Behaviors
{
    public class Resetting : IMovementBehavior
    {
        private float speed = 3f;
        public (IMovementBehavior behavior, Vector3 movementAdded) GetBehaviorAndMovement()
        {
            var movement = this.MoveToDefaultPos() * this.speed * Time.fixedDeltaTime;
            var behavior = this.GetBehavior();

            return (behavior, movement);
        }


        private Vector3 MoveToDefaultPos() => (Player.Instance.Position - Whatever.Instance.Position).Set(y: 0, z: 0);


        private IMovementBehavior GetBehavior()
        {
            // check if condition for walkTowardOther is fulfilled
            var closestBigDog = BigDog.GetClosest();
            if (closestBigDog != null)
                return new WalkTowardOther();
            else
                return this;

            // check if condition for oscillation is fulfilled    
        }
    }
}
