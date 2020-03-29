using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomScripts.Fundamentals;
using UnityEngine;
using CustomScripts.Managers;

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
                _amount = Mathf.Clamp(value, 0, Movement.maxCurb);
            }
        }
        public CurbGauge()
        {
            this.Amount = Movement.maxCurb;
            UpdateManager.Instance.GlobalUpdate += this.RecoverGauge;
        }


        public float GetCurbValue()
        {
            var slowSpeed = 2f;
            var verticalInput = Input.GetAxis("Vertical");
            var curbValue = verticalInput * slowSpeed * Time.deltaTime;
            curbValue = (curbValue < this.Amount) ? curbValue : this.Amount;
            curbValue = Mathf.Abs(curbValue);

            this.Amount -= curbValue;
            Debug.Log(Amount);
            return this.Amount > 0 ? curbValue : 0;
        }


        public void RecoverGauge()
        {
            var isNotCurbing = Input.GetAxisRaw("Vertical") == 0;
            var recoverAmount = Time.deltaTime / 3;
            if (isNotCurbing)
                this.Amount += recoverAmount;
        }
    }
}
