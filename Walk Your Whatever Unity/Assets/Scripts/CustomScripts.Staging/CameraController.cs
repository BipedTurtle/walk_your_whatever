﻿using UnityEngine;
using CustomScripts.Fundamentals;
using CustomScripts.Entities;
using CustomScripts.Managers;

namespace CustomScripts.Staging
{
    public class CameraController : MonoBehaviour
    {
        public static Camera main { get; private set; }
        private void Start()
        {
            main = Camera.main;

            UpdateManager.Instance.GlobalLateUpdate += this.TrackDownWhatever;
        }


        [SerializeField] private float verticalOffset = 10f;
        private void TrackDownWhatever()
        {
            var whatever = Whatever.Instance.transform;

            var cameraY = whatever.position.y + this.verticalOffset;
            var cameraZ = whatever.position.z;
            var newPos = transform.position.Set(y: cameraY, z: cameraZ);

            transform.position = newPos;
        }
    }
}