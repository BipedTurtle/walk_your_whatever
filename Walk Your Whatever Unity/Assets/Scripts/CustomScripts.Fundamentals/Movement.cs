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

        private void ResetCurb() => Movement.Curb = 0;

        private Vector3 value;
        public Movement()
        {
            this.value = Vector3.zero;
            GameManager.Instance.GameOver += this.ResetCurb;
        }

        public Movement(Vector3 movementVal)
        {
            this.value = movementVal;
            GameManager.Instance.GameOver += this.ResetCurb;
        }


        public void Add(Vector3 value) => this.value += value;

        public void Add(Movement externalMovement, float curbFactor=1f) => this.value += externalMovement.value * curbFactor;

        public void Move(Transform objectMoving)
        {
            // move sideways
            objectMoving.position += this.value;

            // default forward movement
            var forwardMovement = GetDefaultForwardMovement(Movement.Curb);
            objectMoving.position += forwardMovement;
        }

        private static float forwardSpeed = 3f;
        public static Vector3 GetDefaultForwardMovement(float curb = 0) =>
            Vector3.forward * forwardSpeed *(1 - curb) * Time.fixedDeltaTime;
    }
}
