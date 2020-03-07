using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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


    }
}
