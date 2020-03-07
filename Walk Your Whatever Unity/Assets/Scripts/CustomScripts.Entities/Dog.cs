using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Fundamentals;
using CustomScripts.Managers;

namespace CustomScripts.Entities
{
    public class Dog : Whatever
    {
        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            UpdateManager.Instance.GlobalFixedUpdate += this.Move;   
        }

        Func<float, float> movementFunc = (t) => 2 * Mathf.Sin(t);
        private void Move()
        {
            var speed = 3f;
            transform.position = transform.position.Set(x: this.movementFunc(Time.time * speed));
        }
    }
}
