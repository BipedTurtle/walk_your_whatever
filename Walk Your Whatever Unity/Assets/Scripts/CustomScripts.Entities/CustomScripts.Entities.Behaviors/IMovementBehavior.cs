using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomScripts.Entities.Behaviors
{
    public interface IMovementBehavior
    {
        (IMovementBehavior behavior, Vector3 movementAdded) GetBehaviorAndMovement();
    }
}
