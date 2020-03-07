using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Managers;

namespace CustomScripts.Entities
{
    [RequireComponent(typeof(LineRenderer))]
    public class Leash : MonoBehaviour
    {
        private LineRenderer leash;
        private void Start()
        {
            UpdateManager.Instance.GlobalUpdate += this.Extend;

            this.leash = GetComponent<LineRenderer>();
        }

        private void Extend()
        {
            var playerPos = Player.Instance.Position;
            var whateverPos = Whatever.Instance.Position;

            var startAndEnd = new Vector3[2];
            this.leash.GetPositions(startAndEnd);
            var start = startAndEnd[0];
            var end = startAndEnd[1];

            start = playerPos;
            end = whateverPos;
            this.leash.SetPositions(new Vector3[] { start, end });
        }
    }
}
