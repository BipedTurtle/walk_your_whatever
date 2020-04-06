using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Managers;

namespace CustomScripts.Environment
{
    public class Obstacle : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var isDogOrPlayer = other.transform.tag == "Dog" || other.transform.tag == "Player";
            if (isDogOrPlayer)
                GameManager.Instance.OnGameOver();       
        }
    }
}