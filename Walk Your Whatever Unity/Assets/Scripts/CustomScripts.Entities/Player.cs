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
        }
        #endregion

        public Vector3 Position { get => transform.position; }

        [SerializeField] private Leash leashPrefab;

        private void Start()
        {
            UpdateManager.Instance.GlobalLateUpdate += this.Move;

            this.GetLeash();
        }

        private void GetLeash()
        {
            var leash = Instantiate(this.leashPrefab, this.Position, Quaternion.identity);
            leash.transform.SetParent(this.transform);
        }

        private void Move()
        {
            var pullFactor = .01f;
            var movement = (Whatever.Instance.Position - this.Position).Set(y: 0, z: 0) * pullFactor;
            transform.position += movement;
        }
    }
}
