using UnityEngine;
using CustomScripts.Entities;
using CustomScripts.Managers;

namespace CustomScripts.Fundamentals
{
    public class MovementAnalytics : MonoBehaviour
    {
        private static float totalVerticalDisplacement;
        private static float TotalOscillation;

        public static void MeasureOscillation(float oscillationAmount) => TotalOscillation += oscillationAmount;

        private void Start()
        {
            this.previousPlayerPos = Player.Instance.Position;
            this.currentPlayerPos = Player.Instance.Position;

            UpdateManager.Instance.GlobalFixedUpdate += this.MeasureVerticalMovement;
        }


        private Vector3 previousPlayerPos;
        private Vector3 currentPlayerPos;
        public void MeasureVerticalMovement()
        {
            this.previousPlayerPos = this.currentPlayerPos;
            this.currentPlayerPos = Player.Instance.Position;

            var movementThisFrame = (this.currentPlayerPos - previousPlayerPos).Set(x:0,y:0).magnitude;
            totalVerticalDisplacement += movementThisFrame;
        }


        private void OnApplicationQuit()
        {
            Debug.Log($"Vertical Displacement: {totalVerticalDisplacement}\nTotal Oscillation: {TotalOscillation}");
            var ratio = TotalOscillation / totalVerticalDisplacement;
            Debug.Log($"Ratio: {ratio} oscillations per unity unit");
        }
    }
}
