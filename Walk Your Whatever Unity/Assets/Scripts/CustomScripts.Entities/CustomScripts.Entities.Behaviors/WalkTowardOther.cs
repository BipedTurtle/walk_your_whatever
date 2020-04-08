using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Environment;
using CustomScripts.Managers;

namespace CustomScripts.Entities.Behaviors
{
    public class WalkTowardOther : IMovementBehavior
    {
        private List<BigDog> otherDogs = new List<BigDog>();
        public WalkTowardOther()
        {
            this.otherDogs = GameManager.FindObjectsOfType<BigDog>().ToList();
        }


        private Transform target;
        public (IMovementBehavior behavior, Vector3 movementAdded) GetBehaviorAndMovement()
        {
            this.target = this.SetTarget();
            var towardTargetMovement = (this.target.position - Whatever.Instance.Position) * Time.fixedDeltaTime;

            return (this, towardTargetMovement);
        }

        private Transform SetTarget() =>
            this.target =
                this.otherDogs
                    .Select(d => new { Dog = d, Distance = (d.transform.position - Whatever.Instance.Position).sqrMagnitude })
                    .Aggregate(func: (current, next) => current.Distance > next.Distance ? next : current)
                    .Dog.transform;
    }
}
