using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Managers;
using System.Collections;

namespace CustomScripts.Fundamentals
{
    public class Movement
    {
        public static float maxCurb { get; } = .3f;
        private static float _curb;
        public static float Curb
        {
            get => _curb;
            set
            {
                _curb = Mathf.Clamp(value, -maxCurb, maxCurb);
            }
        }

        public static void CurbSpeed(float curbAmount)
        {
            Movement.Curb += curbAmount;
            if (GameManager.Instance != null)
                GameManager.Instance.StartCoroutine(CancelOutCurb());

            IEnumerator CancelOutCurb(float after = 2f)
            {
                yield return new WaitForSeconds(after);
                Movement.Curb -= curbAmount;
            }
        }


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

        public void Move(Transform objectMoving)
        {
            // move sideways
            objectMoving.position += this.value;

            // default forward movement
            var forwardMovement = this.GetDefaultForwardMovement();
            objectMoving.position += forwardMovement;
        }

        private float forwardSpeed = 3f;
        public Vector3 GetDefaultForwardMovement() =>
            Vector3.forward* forwardSpeed *(1 - Movement.Curb) * Time.fixedDeltaTime;
    }
}
