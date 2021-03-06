﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Entities;
using CustomScripts.Managers;

namespace CustomScripts.Environment
{
    public class Tree : Obstacle
    {
        [SerializeField] private float fallDistance = 4f;

        protected override void Start()
        {
            base.Start();

            this.animator = GetComponent<Animator>();

            UpdateManager.Instance.GlobalUpdate += this.Fall;
        }


        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
        }


        private Animator animator;
        private void Fall()
        {
            var distanceToPlayer = Mathf.Abs(Player.Instance.Position.z - transform.position.z);
            if (distanceToPlayer < this.fallDistance) {
                this.animator.SetTrigger("Fall");
                UpdateManager.Instance.GlobalUpdate -= this.Fall;
            }
        }   
    }
}
