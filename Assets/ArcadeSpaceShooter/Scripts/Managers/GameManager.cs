using System.Collections;
using ArcadeShooter.GameJobs;
using ArcadeShooter.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArcadeShooter.Managers{
    public enum GameState{
        Ready,
        Starting,
        Playing,
        End
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        //UI Elements

        //Game State
        private GameState gameState;

        private Transform player;
        [SerializeField]private EnemySpawnerJob enemySpawner;

        private float score;
        private float timeElapsed;

        void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            player = FindObjectOfType<PlayerManager>().transform;
            enemySpawner = FindObjectOfType<EnemySpawnerJob>();

            gameState = GameState.Ready;
        }

        void Start()
        {
            gameState = GameState.Starting;
            StartCoroutine(MainGameLoopRoutine());
        }

        IEnumerator MainGameLoopRoutine()
        {
            yield return StartCoroutine(StartGameRoutine());
            yield return StartCoroutine(PlayGameRoutine());
            yield return StartCoroutine(EndGameRoutine());
        }

        IEnumerator StartGameRoutine()
        {
            timeElapsed = 0f;
            PlayerManager.EnablePlayer(true);

            yield return new WaitForSeconds(2f);
            gameState = GameState.Playing;
        }

        void Update()
        {
            if(gameState == GameState.Starting || gameState == GameState.Playing)
            {
                UpdateTime();
            }
        }

        IEnumerator PlayGameRoutine()
        {
            enemySpawner?.StartSpawn();
            while(gameState == GameState.Playing)
            {
                yield return null;
            }
        }

        void UpdateTime()
        {
            timeElapsed += Time.deltaTime;
        }

        IEnumerator EndGameRoutine()
        {
            yield return new WaitForSeconds(2f);

            gameState = GameState.Ready;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public static Vector3 GetPlayerPosition()
        {
            if(GameManager.Instance == null)
            {
                return Vector3.zero;
            }

            return (Instance.player != null) ? GameManager.Instance.player.position : Vector3.zero;
        }

        public static void EndGame()
        {
            if(GameManager.Instance == null)
            {
                return;
            }
            PlayerManager.EnablePlayer(false);
            Instance.gameState = GameState.End;
        }

        public static bool IsGameOver()
        {
            if(GameManager.Instance == null)
            {   
                return false;
            }
            return (Instance.gameState == GameState.End);
        }

        public static void AddScore(int scoreValue)
        {
            Instance.score += scoreValue;
        }


    }
}
