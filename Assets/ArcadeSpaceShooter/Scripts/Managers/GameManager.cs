using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcadeShooter.Managers{
    public class GameManager : MonoBehaviour
    {
        static int score = 0;
        static bool isGameOver = false;

        public static GameManager Instance;

        public static int GetScore()
        {
            return score;
        }

        public static bool IsGameOver()
        {
            return isGameOver;
        }

        public static bool GameOver(bool value)
        {
            isGameOver = value;
            return isGameOver;
        }

        private void Awake()
        {
            GMInitializer();
        }

        private void GMInitializer()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void AddToScore(int scoreValue)
        {
            score += scoreValue;
        }

        public void ResetGame()
        {
            Destroy(gameObject);
        }
    }
}
