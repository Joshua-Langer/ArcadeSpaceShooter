using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Collections;
using Random = UnityEngine.Random;

namespace ArcadeShooter.Managers{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Spawner")]
        [SerializeField] int spawnCount = 30;
        [SerializeField] float spawnInterval = 3f;
        [SerializeField] float spawnRadius = 30f;
        [SerializeField] int difficultyBonus = 5;

        [Header("Enemy")]
        [SerializeField] float minSpeed = 4f;
        [SerializeField] float maxSpeed = 12f;

        float spawnTimer = 0f;
        EntityManager entityManager;

        [SerializeField] Mesh enemyMesh;
        [SerializeField] Material enemyMaterial;

        private bool canSpawn = false;
        [SerializeField] GameObject enemyPrefab;

        Entity enemyEntityPrefab;

        void Start()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
            enemyEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(enemyPrefab, settings);
            SpawnWave();
        }

        void SpawnWave()
        {
            NativeArray<Entity> enemyArray = new NativeArray<Entity>(spawnCount, Allocator.Temp);

            for(var i = 0; i < enemyArray.Length; i++)
            {
                enemyArray[i] = entityManager.Instantiate(enemyEntityPrefab);
                entityManager.SetComponentData(enemyArray[i], new Translation{Value = RandomPointOnCircle(spawnRadius)});
                entityManager.SetComponentData(enemyArray[i], new MoveForward {speed = Random.Range(minSpeed, maxSpeed)});
            }

            enemyArray.Dispose();
            spawnCount += difficultyBonus;
        }

        float3 RandomPointOnCircle(float radius)
        {
            Vector2 randomPoint = Random.insideUnitCircle.normalized * radius;
            
            return new float3(randomPoint.x, 0.5f, randomPoint.y) + (float3)GameManager.GetPlayerPosition();
        }

        public void StartSpawn()
        {
            canSpawn = true;
        }

        void Update()
        {
            if(!canSpawn || GameManager.IsGameOver())
            {
                return;
            }
            spawnTimer += Time.deltaTime;

            if(spawnTimer > spawnInterval)
            {
                SpawnWave();
                spawnTimer = 0;
            }
        }
    }
}
