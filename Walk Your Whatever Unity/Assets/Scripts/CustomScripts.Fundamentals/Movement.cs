using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomScripts.Fundamentals
{
    public class Movement
    {
        private Vector3 value;

        public Movement()
        {
            this.value = Vector3.zero;
        }

        public Movement(Vector3 movementVal)
        {
            this.value = movementVal;
        }

        public void Add(Vector3 value) => this.value += value;

        public void Add(Movement externalMovement, float curbFactor=1f) => this.value += externalMovement.value * curbFactor;

        public void Move(Transform objectMoving) => objectMoving.position += this.value;
    }
}
