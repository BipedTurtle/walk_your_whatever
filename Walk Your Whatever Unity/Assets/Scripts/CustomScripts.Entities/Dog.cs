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

            this.movementBehavior = new WalkTowardOther();
            //this.movementBehavior = new OscillateSideways();
            //this.movementBehavior = new StraightWalking();

            base.totalMovement = new Movement();
        }


        private void Start()
        {
            GameManager.Instance.GameOver += this.OnGameOver_Stop;

            UpdateManager.Instance.GlobalFixedUpdate += this.AddMovementInherent;
            UpdateManager.Instance.GlobalFixedUpdate += base.GetInfluencedByPlayerMovement;
            UpdateManager.Instance.GlobalFixedUpdate += this.Walk;
        }


        private void OnGameOver_Stop()
        {
            UpdateManager.Instance.GlobalFixedUpdate -= this.AddMovementInherent;
            UpdateManager.Instance.GlobalFixedUpdate -= base.GetInfluencedByPlayerMovement;
            UpdateManager.Instance.GlobalFixedUpdate -= this.Walk;
        }



        private void Walk() => this.totalMovement.Move(transform);


        private IMovementBehavior movementBehavior;
        private void AddMovementInherent() 
        {
            var behavior = this.movementBehavior.GetBehaviorAndMovement();

            this.movementBehavior = behavior.behavior;
            base.totalMovement = new Movement(behavior.movementAdded);
        }
    }
}