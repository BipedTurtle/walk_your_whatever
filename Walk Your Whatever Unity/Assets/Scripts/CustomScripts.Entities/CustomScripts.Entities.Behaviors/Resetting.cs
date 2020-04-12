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
        private float speed = 4f;
        public (IMovementBehavior behavior, Vector3 movementAdded) GetBehaviorAndMovement()
        {
            var movement = this.MoveToDefaultPos() * this.speed * Time.fixedDeltaTime;
            var behavior = this.GetBehavior();

            return (behavior, movement);
        }


        private Vector3 MoveToDefaultPos() => (Player.Instance.Position - Whatever.Instance.Position).Set(y: 0, z: 0);


        private float threshold = .13f;
        private float RemainingDistance => Mathf.Abs(Player.Instance.Position.x - Whatever.Instance.Position.x);
        private IMovementBehavior GetBehavior()
        {
            var closestBigDog = BigDog.GetClosest();
            if (closestBigDog != null)
                return new WalkTowardOther();

            if (this.RemainingDistance < this.threshold)
                return new OscillateSideways(Dog.MovmentThisFrame);
            else
                return this;
        }
    }
}
