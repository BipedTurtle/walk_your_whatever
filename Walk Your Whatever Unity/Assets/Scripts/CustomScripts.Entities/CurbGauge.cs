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
            if (this.Amount <= 0)
                return 0;

            var slowSpeed = 2f;
            var verticalInput = Input.GetAxis("Vertical");
            //Debug.Log($"vertical input: {verticalInput}");
            var curbValue = verticalInput * slowSpeed * Time.fixedDeltaTime;

            var curbAmount = Mathf.Abs(curbValue);
            curbAmount = (curbAmount < this.Amount) ? curbAmount : this.Amount;
            this.Amount -= curbAmount;
            var sign = verticalInput > 0 ? -1 : 1;
            return sign * curbAmount;
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
