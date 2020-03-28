using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomScripts.Entities.Behaviors
{
    public class FixedPointWalking : IMovementBehavior
    {
        private IMovementBehavior nextBehavior;
        public FixedPointWalking()
        {
            this.nextBehavior = this;
        }


        public (IMovementBehavior behavior, Vector3 movementAdded) GetBehaviorAndMovement()
            => (this.nextBehavior, Vector3.zero);
    }
}
