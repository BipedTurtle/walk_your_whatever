using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Fundamentals;

namespace CustomScripts.Entities
{
    public class Whatever : MonoBehaviour
    {
        #region Singleton
        public static Whatever Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
        #endregion

        public Vector3 Position { get => transform.position; }

        protected Movement totalMovement;
        #region private float curb;
        [Tooltip("the extent to which Whatever's movement is influenced by Player's movement")]
        [SerializeField]
        [Range(0, 3)]
        private float curb = 1f;
        #endregion  
        protected void GetInfluencedByPlayerMovement()
        {
            var playerMovement = Player.Instance.PlayerMovement;
            this.totalMovement.Add(playerMovement, curbFactor: this.curb);
        }
    }
}
