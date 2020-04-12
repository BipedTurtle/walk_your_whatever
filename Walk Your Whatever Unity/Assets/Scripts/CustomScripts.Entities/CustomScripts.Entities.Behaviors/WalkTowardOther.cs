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


        // the diagonal of a 4x4 square
        public static float attractionDistanceThreshold => Mathf.Sqrt(32);
        private Vector3 ToTarget => this.target == null ? Vector3.zero : this.target.position - Whatever.Instance.Position;
        private bool TargetWithinRange => (Whatever.Instance.Position.z < this.target.position.z) && (ToTarget.magnitude < attractionDistanceThreshold);
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
            Vector3 horizontalMovement = Vector3.right * this.ToTarget.normalized.x;
            Vector3 horizontalBias = Vector3.right * this.ToTarget.x * .18f;
            Vector3 movement = horizontalMovement + horizontalBias;

            return
                this.TargetWithinRange ?
                    movement * speed * Time.fixedDeltaTime :
                    Vector3.zero;
        }
    }
}