using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CustomScripts.Environment;
using CustomScripts.Managers;

namespace CustomScripts.Entities.Behaviors
{
    public class WalkTowardOther : IMovementBehavior
    {
        private Transform target;
        public (IMovementBehavior behavior, Vector3 movementAdded) GetBehaviorAndMovement()
        {
            if (this.target == null)
                this.target = this.SetTarget();

            var movement = this.GetMovementToTarget();
            var behavior = this.LoseTargetIfOutOfSight();
            return (behavior, movement);
        }


        private Transform SetTarget()
        {
            var thereAreNoOtherDogs = BigDog.BigDogs.Count == 0;
            if (thereAreNoOtherDogs)
                return null;

            return 
                this.target =
                    BigDog.BigDogs
                        .Select(d => new { Dog = d, Distance = (d.transform.position - Whatever.Instance.Position).sqrMagnitude })
                        .Aggregate(func: (current, next) => current.Distance > next.Distance ? next : current)
                        .Dog.transform;
        }


        private float targetDistanceThreshold = Mathf.Sqrt(3 * 3 + 3 * 3);
        private Vector3 ToTarget => this.target == null ? Vector3.zero : this.target.position - Whatever.Instance.Position;
        private bool TargetWithinRange => ToTarget.magnitude < targetDistanceThreshold;
        private IMovementBehavior LoseTargetIfOutOfSight()
        {
            bool targetOutOfRange = !this.TargetWithinRange;
            this.target = (targetOutOfRange) ? null : this.target;
            if (targetOutOfRange)
                return new Resetting();
            else
                return this;
        }


        // this might not work in a curved map since we're using position.z here
        private Vector3 GetMovementToTarget()
        {
            if (this.target == null)
                return Vector3.zero;

            var hasPassedOther = Whatever.Instance.Position.z > this.target.position.z;
            float speed = hasPassedOther ? .9f : 1.3f;
            Vector3 horizontalBias = Vector3.right * this.ToTarget.x * .48f;
            Vector3 verticalBias = Vector3.back * this.ToTarget.z * .3f;
            Vector3 movement = this.ToTarget.normalized + horizontalBias + verticalBias;
            return
                this.TargetWithinRange ?
                    movement * speed * Time.fixedDeltaTime :
                    Vector3.zero;
        }
    }
}