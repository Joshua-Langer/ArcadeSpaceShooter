using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArcadeShooter.Managers{
    public class GameManager : MonoBehaviour
    {
        static int score = 0;
        static bool isGameOver = false;
        static int playerLives = 3;

        public static GameManager Instance;

        public static int GetScore()
        {
            return score;
        }

        public static int GetPlayerLives()
        {
            return playerLives;
        }

        public static void AddPlayerLives(int life)
        {
            playerLives += life;
        }

        public static bool IsGameOver()
        {
            return isGameOver;
        }

        public static void GameOver(bool value)
        {
            isGameOver = value;
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

        public static void AddToScore(int scoreValue)
        {
            score += scoreValue;
            Debug.Log("Player Score is " + score);
        }

        public void ResetGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            playerLives--;
            isGameOver = false;
            Debug.Log("Player has lost a life, they are at " + playerLives);
        }

        void Update()
        {
            if(isGameOver)
            {
                if(playerLives == 0) {Debug.Log("Player out of lives, scoreboard will show here.");}
                if(playerLives > 0) {ResetGame();} 
            }
        }
    }
}
