using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Managers;
using CustomScripts.Entities;
using CustomScripts.Fundamentals;

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


        // the diagonal of a 3x3 square
        private static float closeThreshold = Mathf.Sqrt(18f);
        public static BigDog GetClosest()
        {
            var dogInfo =
                BigDog.BigDogs
                    .Select(dog => new { bigDog = dog, distance = (dog.transform.position - Player.Instance.Position).Set(y: 0).magnitude })
                    .Aggregate((current, next) => (next.distance < current.distance) ? next : current);

            return
                (dogInfo.distance < BigDog.closeThreshold) ?
                    dogInfo.bigDog :
                    null;
        }

        private void Bite()
        {
               
        }


        private void RegsiterBigDog() => BigDog.BigDogs.Add(this);
    }
}
