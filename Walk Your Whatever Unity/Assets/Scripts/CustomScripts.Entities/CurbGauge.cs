using CustomScripts.Fundamentals;
using UnityEngine;
using CustomScripts.Managers;

namespace CustomScripts.Entities
{
    public class CurbGauge
    {
        private float _amount;
        public float Amount
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
            UIManager.Instance.CurbGauge = this;

            UpdateManager.Instance.GlobalUpdate += this.RecoverGauge;
        }


        public float GetCurbValue()
        {
            if (this.Amount <= 0)
                return 0;

            var slowSpeed = 2f;
            var verticalInput = Input.GetAxis("Vertical");
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
            var recoverAmount = Time.deltaTime / 25;
            if (isNotCurbing)
                this.Amount += recoverAmount;
        }
    }
}
