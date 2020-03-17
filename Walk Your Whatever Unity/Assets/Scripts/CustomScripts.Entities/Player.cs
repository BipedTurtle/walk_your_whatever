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
            UpdateManager.Instance.GlobalFixedUpdate += this.GetKeyboardInputs;
            UpdateManager.Instance.GlobalFixedUpdate += this.Move;

            this.GetLeash();
        }

        private void GetLeash()
        {
            var leash = Instantiate(this.leashPrefab, this.Position, Quaternion.identity);
            leash.transform.SetParent(this.transform);
        }

        public Movement PlayerMovement { get; private set; }
        private void GetKeyboardInputs()
        {
            var horizontalMovement = Input.GetAxis("Horizontal");
            var movementVector = Vector3.right * horizontalMovement;
            this.PlayerMovement = new Movement(movementVector);
        }

        private void Move()
        {
            var pullFactor = .01f;
            var movementByPull = (Whatever.Instance.Position - this.Position).Set(y: 0, z: 0) * pullFactor;
            this.PlayerMovement.Add(movementByPull);

            this.PlayerMovement.Move(transform);
        }
    }
}
