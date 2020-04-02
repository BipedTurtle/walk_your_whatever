using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace CustomScripts.CustomUI
{
    public class Gauge : MonoBehaviour
    {
        [SerializeField] private Image fillUI;

        public void UpdateGauge(float newFill)
        {
            this.fillUI.fillAmount = newFill;
        }
    }
}
