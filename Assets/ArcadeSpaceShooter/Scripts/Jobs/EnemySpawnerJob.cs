using ArcadeShooter.Managers;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Jobs;

namespace ArcadeShooter.GameJobs{
public class EnemySpawnerJob : MonoBehaviour
{
        [Header("Bounds")]
        public float topBound = 0f;
        public float botBound = 0f;
        public float leftBound = 0f;
        public float rightBound = 0f;

        [Header("Enemy Settings")]
        public GameObject enemyShipPrefab;
        [SerializeField] float minSpeed = 4f;
        [SerializeField] float maxSpeed = 12f;
        
        [Header("Spawn Settings")]
        public int enemyShipCount = 1;
        public float enemyShipInterval = 3f;
        public int difficultyBonus = 5;

        float spawnTimer = 0f;

        EntityManager entityManager;
        [SerializeField] Mesh enemyMesh;
        [SerializeField] Material enemyMaterial;

        Entity enemyEntityPrefab;

        private bool canSpawn = false;

        TransformAccessArray transforms;
        MovementJob movementJob;
        JobHandle moveHandle;

        void OnDisable()
        {
            moveHandle.Complete();
            transforms.Dispose();
        }
        

        public void Start()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            transforms = new TransformAccessArray(0, -1);
            var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
            enemyEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(enemyShipPrefab, settings);
            SpawnWave();
        }

        void SpawnWave()
        {
            NativeArray<Entity> enemyArray = new NativeArray<Entity>(enemyShipCount, Allocator.Temp);

            transforms.capacity = transforms.length + enemyShipCount;

            for(var i = 0; i < enemyArray.Length; i++)
            {
                float xVal = Random.Range(leftBound, rightBound);
                float zVal = Random.Range(0f, 10f);
                Vector3 pos = new Vector3(xVal, 0f, zVal + topBound);
                Quaternion rot = Quaternion.Euler(0f, 180f, 0f);

                enemyArray[i] = entityManager.Instantiate(enemyEntityPrefab);
                entityManager.SetComponentData(enemyArray[i], new Translation{Value = pos});
                entityManager.SetComponentData(enemyArray[i], new MoveForward {speed = Random.Range(minSpeed, maxSpeed)});
                //transforms.Add(enemyArray[i].transform);
            }

            enemyArray.Dispose();
            moveHandle.Complete();
            
            enemyShipCount += difficultyBonus;
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
            moveHandle.Complete();
            if(spawnTimer > enemyShipInterval)
            {
                SpawnWave();
                spawnTimer = 0;
            }

            movementJob = new MovementJob()
            {
                moveSpeed = Random.Range(minSpeed, maxSpeed),
                topBound = topBound,
                bottomBound = botBound,
                deltaTime = Time.deltaTime
            };

            moveHandle = movementJob.Schedule(transforms);
            JobHandle.ScheduleBatchedJobs();
        }


    }
}
