using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Managers;
using CustomScripts.Entities;
using CustomScripts.Fundamentals;
using CustomScripts.Entities.Behaviors;

namespace CustomScripts.Environment
{
    public class BigDog : MonoBehaviour
    {
        [SerializeField] private float bitingDistance = Mathf.Sqrt(2);
        public static List<BigDog> BigDogs { get; } = new List<BigDog>();


        private void Start()
        {
            this.RegsiterBigDog();

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

        private void Bite()
        {
               
        }


        private void RegsiterBigDog() => BigDog.BigDogs.Add(this);
    }
}
