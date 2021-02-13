using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcadeShooter.Managers{
    public class FXManager : ObjectPooler
    {
        public static FXManager Instance;

        [Space]

        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private string explosionTag = "Explosion";
        [SerializeField] private int poolSize = 40;

        protected virtual void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            if(explosionPrefab != null)
            {
                ObjectPoolItem explosionPoolItem = new ObjectPoolItem
                {
                    objectToPool = explosionPrefab,
                    amountToPool = poolSize,
                    shouldExpand = true
                };
                itemsToPool.Add(explosionPoolItem);
            }
        }

        protected override void Start()
        {
            base.Start();
        }

        public void CreateExplosion(Vector3 pos)
        {
            GameObject instance = GetPooledObject(explosionTag);
            if(instance != null)
            {
                instance.SetActive(false);
                instance.transform.position = pos;
                instance.SetActive(true);
            }
        }
    }   
   
}
