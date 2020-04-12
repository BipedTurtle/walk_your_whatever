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
        }


        public static BigDog GetClosest()
        {
            var bigDogInfo =
                BigDog.BigDogs
                    .Select(dog => new { bigDog = dog, playerToDogDistance = (dog.transform.position - Player.Instance.Position).Set(y: 0).magnitude })
                    .Aggregate((current, next) => (next.playerToDogDistance < current.playerToDogDistance) ? next : current);

            bool attractionCondition =
                bigDogInfo.playerToDogDistance < WalkTowardOther.targetDistanceThreshold &&
                Whatever.Instance.Position.z < bigDogInfo.bigDog.transform.position.z;

            return
                (attractionCondition) ?
                    bigDogInfo.bigDog :
                    null;
        }


        private Vector3 ToDogVector => Dog.Instance.Position - transform.position;
        private float biteThreshold = Mathf.Sqrt(4);
        private float lungeSpeed = 12f;
        private void Bite()
        {
            var toDogDistance = this.ToDogVector.magnitude;
            if (toDogDistance < this.biteThreshold) {
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
                //Debug.Log(distanceRemaining);
                if (distanceRemaining > exactBiteThreshold)
                    StartCoroutine(BitingAction());
                //else
                //    GameManager.Instance.OnGameOver();
            }
        }


        private void Disable()
        {
            UpdateManager.Instance.GlobalUpdate -= this.Bite;
        }


        private void RegsiterBigDog() => BigDog.BigDogs.Add(this);
    }
}
