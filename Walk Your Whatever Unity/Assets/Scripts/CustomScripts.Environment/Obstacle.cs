using UnityEngine;
using CustomScripts.Managers;
using CustomScripts.Staging;

namespace CustomScripts.Environment
{
    public class Obstacle : MonoBehaviour
    {
        protected virtual void OnTriggerEnter(Collider other)
        {
            var isDogOrPlayer = other.transform.tag == "Dog" || other.transform.tag == "Player";
            if (isDogOrPlayer)
                GameManager.Instance.OnGameOver();       
            
        }


        protected virtual void Start()
        {
            UpdateManager.Instance.GlobalUpdate += this.ToggleObstacle;
        }


        private void ToggleObstacle()
        {
            Vector2 viewportPos = CameraController.main.WorldToViewportPoint(transform.position);
            bool isOutOfBounds = OutOfViewportBounds(viewportPos);
            if (isOutOfBounds)
                gameObject.SetActive(false);
            else
                gameObject.SetActive(true);


            bool OutOfViewportBounds(Vector2 viewport)
            {
                Vector2 lowerBound = new Vector2(-1, -1);
                Vector2 upperBound = new Vector2(1, 1.5f);

                var isOutofBounds =
                    (viewport.x < lowerBound.x || viewport.x > upperBound.x) ||
                    (viewport.y < lowerBound.y || viewport.y > upperBound.y);

                return isOutofBounds;
            }
        }
    }
}