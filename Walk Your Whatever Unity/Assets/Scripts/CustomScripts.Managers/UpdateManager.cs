using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomScripts.Managers
{
    public class UpdateManager : MonoBehaviour
    {
        #region Singleton
        public static UpdateManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
        #endregion

        public event Action GlobalUpdate;
        public event Action GlobalFixedUpdate;
        public event Action GlobalLateUpdate;

        private void Update()
        {
            this.GlobalUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            this.GlobalFixedUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            this.GlobalLateUpdate?.Invoke();
        }
    }
}
