using UnityEngine;
using CustomScripts.Fundamentals;
using CustomScripts.Managers;

namespace CustomScripts.Entities
{
    public class Player : MonoBehaviour
    {
        #region Singleton
        public static Player Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;


            this.PlayerMovement = new Movement();
        }
        #endregion

        public Vector3 Position { get => transform.position; }

        [SerializeField] private Leash leashPrefab;

        private void Start()
        {
            this.curbGauge = new CurbGauge();
            this.defaultVertical = (Whatever.Instance.Position - this.Position).Set(x:0, y: 0);

            GameManager.Instance.GameOver += this.OnGameOver_Stop;

            UpdateManager.Instance.GlobalFixedUpdate += this.GetKeyboardInputs;
            UpdateManager.Instance.GlobalFixedUpdate += this.Move;

            this.GetLeash();
        }


        private void OnGameOver_Stop()
        {
            UpdateManager.Instance.GlobalFixedUpdate -= this.GetKeyboardInputs;
            UpdateManager.Instance.GlobalFixedUpdate -= this.Move;
        }


        private void GetLeash()
        {
            var leash = Instantiate(this.leashPrefab, this.Position, Quaternion.identity);
            leash.transform.SetParent(this.transform);
        }

        public Movement PlayerMovement { get; private set; }
        private CurbGauge curbGauge;
        private void GetKeyboardInputs()
        {
            var horizontalMovement = Input.GetAxis("Horizontal") * Vector3.right;
            var curbAmount = this.curbGauge.GetCurbValue();
            Movement.CurbSpeed(curbAmount);

            this.PlayerMovement = new Movement(horizontalMovement);
        }


        private Vector3 defaultVertical;
        private Vector3 ToWhatever => (Whatever.Instance.Position - this.Position).Set(y:0);
        private void Move()
        {
            GetPulledByWhatever();

            var player = this.transform;
            this.PlayerMovement.Move(player);


            void GetPulledByWhatever()
            {
                var pullFactor = .03f;

                Vector3 sideMovementByPull = this.ToWhatever.Set(y: 0, z: 0) * pullFactor;

                Vector3 verticalForce = (this.ToWhatever - this.defaultVertical).Set(x:0, y:0);
                verticalForce = verticalForce.magnitude > .01f ? verticalForce : Vector3.zero;
                Vector3 verticalMovementByPull = verticalForce * pullFactor;

                Vector3 movementByPull = sideMovementByPull + verticalMovementByPull;
                this.PlayerMovement.Add(movementByPull);
            }
        }
    }
}
