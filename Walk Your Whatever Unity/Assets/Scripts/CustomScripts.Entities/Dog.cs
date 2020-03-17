using System;
using UnityEngine;
using CustomScripts.Fundamentals;
using CustomScripts.Managers;
using CustomScripts.Entities.Behaviors;

namespace CustomScripts.Entities
{
    public class Dog : Whatever
    {
        protected override void Awake()
        {
            base.Awake();

            this.movementBehavior = new OscillateSideways();
            this.totalMovement = new Movement();
        }


        private void Start()
        {
            UpdateManager.Instance.GlobalFixedUpdate += this.AddMovementInherent;
            UpdateManager.Instance.GlobalFixedUpdate += this.Walk;
        }


        private void Walk() => this.totalMovement.Move(transform);


        private IMovementBehavior movementBehavior;
        private Movement totalMovement;
        private void AddMovementInherent() 
        {
            var behavior = this.movementBehavior.GetBehavior();

            this.movementBehavior = behavior.behavior;
            this.totalMovement = new Movement(behavior.movementAdded);
        }


        public void AddMovementByPlayer(Vector3 movement) => this.totalMovement.Add(movement);
    }
}
