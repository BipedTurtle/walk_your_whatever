using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Managers;

namespace CustomScripts.Environment
{
    public class BigDog : MonoBehaviour
    {
        [SerializeField] private float bitingDistance = Mathf.Sqrt(2);

        private void Start()
        {
            UpdateManager.Instance.GlobalUpdate += this.Bite;
        }
        
        private void Bite()
        {
            
        }
    }
}
