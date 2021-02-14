using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefab;
    [SerializeField] bool looping = false;
    [SerializeField] int obstacleToSpawn;
    [SerializeField] Transform[] spawnerTransform;

    void Awake()
    {
        StartCoroutine(SpawnObstacleWave());
    }

    private IEnumerator SpawnObstacleWave()
    {
        do{
            foreach(GameObject go in obstaclePrefab)
            {
                yield return StartCoroutine(SpawnWave());
            }
        } while(looping);
    }

    private IEnumerator SpawnWave()
    {
        for(int i = 0; i < obstacleToSpawn; i++)
        {
            var newObstacle = Instantiate(obstaclePrefab[Random.Range(0, obstaclePrefab.Length)], spawnerTransform[Random.Range(0, spawnerTransform.Length)].position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }
}
