using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomScripts.Fundamentals;
using UnityEngine;

namespace CustomScripts.Entities
{
    public class CurbGauge
    {
        private float _amount;
        private float Amount
        {
            get => _amount;
            set
            {
                _amount = Mathf.Clamp01(value);
            }
        }
        public CurbGauge()
        {
            this.Amount = Movement.maxCurb;
            Debug.Log(this.Amount);
        }

        public float GetCurbValue()
        {
            var slowSpeed = 2f;
            var verticalInput = Input.GetAxis("Vertical");
            var curbValue = verticalInput * slowSpeed * Time.deltaTime;
            curbValue = (curbValue < this.Amount) ? curbValue : this.Amount;
            curbValue = Mathf.Abs(curbValue);

            this.Amount -= curbValue;
            return this.Amount > 0 ? curbValue : 0;
        }
    }
}
