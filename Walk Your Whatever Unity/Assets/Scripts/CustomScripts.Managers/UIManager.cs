using System;
using UnityEngine;
using CustomScripts.CustomUI;
using CustomScripts.Fundamentals;
using CustomScripts.Entities;

namespace CustomScripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            this.defaultSpeed = Movement.GetDefaultForwardMovement().magnitude;

            UpdateManager.Instance.GlobalUpdate += this.UpdateSpeedGauge;
            UpdateManager.Instance.GlobalUpdate += this.UpdateCurbGauge;
        }


        [SerializeField] private Gauge speedGaugeUI;
        [SerializeField] private Gauge curbGaugeUI;
        private float defaultSpeed;
        private Func<float, float> ratioFunction = (x) => (27.77777f) * x - 1.1666662f;
        private void UpdateSpeedGauge()
        {
            var movement = Movement.GetDefaultForwardMovement(Movement.Curb);
            var speed = movement.magnitude;
            var ratio = this.ratioFunction(speed);
            this.speedGaugeUI.UpdateGauge(ratio);
        }

        public CurbGauge CurbGauge { get; set; }
        private void UpdateCurbGauge()
        {
            var ratio = this.CurbGauge.Amount / Movement.maxCurb;
            this.curbGaugeUI.UpdateGauge(ratio);
        }
    }
}
