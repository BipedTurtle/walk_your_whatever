using UnityEngine;
using CustomScripts.Fundamentals;
using CustomScripts.Managers;
using CustomScripts.Entities.Behaviors;

namespace CustomScripts.Entities
{
    public class Dog : Whatever
    {
        private static Vector3 previousPos = Vector3.zero;
        private static Vector3 currentPos = Vector3.zero;
        public static Vector3 MovmentThisFrame => currentPos - previousPos;

        protected override void Awake()
        {
            base.Awake();

            this.movementBehavior = new Resetting();
            //this.movementBehavior = new WalkTowardOther();
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
            UpdateManager.Instance.GlobalFixedUpdate += this.UpdateMovementPerFrame;
        }


        private void OnGameOver_Stop()
        {
            UpdateManager.Instance.GlobalFixedUpdate -= this.AddMovementInherent;
            UpdateManager.Instance.GlobalFixedUpdate -= base.GetInfluencedByPlayerMovement;
            UpdateManager.Instance.GlobalFixedUpdate -= this.Walk;
        }


        private void UpdateMovementPerFrame()
        {
            Dog.previousPos = Dog.currentPos;
            Dog.currentPos = Dog.Instance.Position;
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