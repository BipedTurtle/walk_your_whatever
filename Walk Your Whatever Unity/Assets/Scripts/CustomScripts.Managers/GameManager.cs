using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomScripts.Entities;

namespace CustomScripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
        #endregion

        private void Start()
        {
            this.player = Player.Instance.transform;
            this.whatever = Whatever.Instance.transform;

            UpdateManager.Instance.GlobalUpdate += this.CheckGameOver;
        }


        public event Action GameOver;
        private Transform player;
        private Transform whatever;
        [SerializeField] private Vector2 boundary = new Vector2(-5, 5);
        private void CheckGameOver()
        {
            var leftBound = this.boundary[0];
            var rightBound = this.boundary[1];

            var playerX = this.player.position.x;
            var playerIsWithinBounds = playerX > leftBound && playerX < rightBound;
            var whateverX = this.whatever.position.x;
            var whateverIsWithinBounds = whateverX > leftBound && whateverX < rightBound;
            var isGameOver = !playerIsWithinBounds || !whateverIsWithinBounds;

            if (isGameOver)
                this.OnGameOver();
        }


        public void OnGameOver()
        {
            this.GameOver?.Invoke();

            var currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name, LoadSceneMode.Single);
        }
    }
}
