using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CustomScripts.Managers;
using CustomScripts.Entities;
using CustomScripts.Fundamentals;
using CustomScripts.Entities.Behaviors;
using UnityEngine.SceneManagement;

namespace CustomScripts.Environment
{
    public class BigDog : MonoBehaviour
    {
        [SerializeField] private float bitingDistance = Mathf.Sqrt(2);
        public static List<BigDog> BigDogs { get; } = new List<BigDog>();


        private void Start()
        {
            this.RegsiterBigDog();
            SceneManager.sceneLoaded += delegate { BigDogs.Clear(); };

            UpdateManager.Instance.GlobalUpdate += this.Bite;
            UpdateManager.Instance.GlobalUpdate += this.DisplayAttraction;
        }


        public static BigDog GetClosest()
        {
            if (BigDog.BigDogs.Count == 0)
                return null;

            var bigDogInfo =
                BigDog.BigDogs
                    .Select(dog => new { bigDog = dog, playerToDogDistance = (dog.transform.position - Dog.Instance.Position).Set(y: 0).magnitude })
                    .Aggregate((current, next) => (next.playerToDogDistance < current.playerToDogDistance) ? next : current);

            bool attractionCondition =
                bigDogInfo.playerToDogDistance < WalkTowardOther.attractionDistanceThreshold &&
                Whatever.Instance.Position.z < bigDogInfo.bigDog.transform.position.z;

            return
                (attractionCondition) ?
                    bigDogInfo.bigDog :
                    null;
        }


        private Vector3 ToDogVector => Dog.Instance.Position - transform.position;
        private float biteThreshold = Mathf.Sqrt(3);
        private bool isWithinBiteRange => ToDogVector.magnitude < biteThreshold;
        private float lungeSpeed = 12f;
        private void Bite()
        {
            if (this.isWithinBiteRange) {
                this.Disable();
                StartCoroutine(BitingAction());
            }

            IEnumerator BitingAction()
            {
                Vector3 lungeThisFrame = this.ToDogVector.normalized * this.lungeSpeed * Time.fixedDeltaTime;
                transform.position += lungeThisFrame.Set(y:0);
                yield return null;

                float distanceRemaining = this.ToDogVector.magnitude;
                var exactBiteThreshold = .5f;
                if (distanceRemaining > exactBiteThreshold)
                    StartCoroutine(BitingAction());
                else
                    GameManager.Instance.OnGameOver();
            }
        }


        [SerializeField] private LineRenderer attraction;
        private bool isWithinAttractionRange =>
            ToDogVector.magnitude < WalkTowardOther.attractionDistanceThreshold &&
            ToDogVector.z < 0;
        private void DisplayAttraction()
        {
            if (this.isWithinAttractionRange) {
                Vector3 startPos = transform.InverseTransformPoint(transform.position);
                Vector3 endPos = transform.InverseTransformPoint(Dog.Instance.Position);
                this.attraction.SetPositions(new Vector3[] { startPos, endPos });
            }
            else
                this.attraction.SetPositions(Enumerable.Repeat(Vector3.zero, count: 2).ToArray());
        }


        private void Disable()
        {
            UpdateManager.Instance.GlobalUpdate -= this.Bite;
            UpdateManager.Instance.GlobalUpdate -= this.DisplayAttraction;
            BigDog.BigDogs.Remove(this);
        }


        private void RegsiterBigDog() => BigDog.BigDogs.Add(this);
    }
}
