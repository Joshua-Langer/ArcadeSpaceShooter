using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcadeShooter.Managers{
    [System.Serializable]
    public class ObjectPoolItem{
        public GameObject objectToPool;
        public int amountToPool;
        public bool shouldExpand;
    }
    public class ObjectPooler : MonoBehaviour
    {
        public List<ObjectPoolItem> itemsToPool;
        public List<GameObject> pooledObjects = new List<GameObject>();

        protected virtual void Start()
        {
            foreach (ObjectPoolItem item in itemsToPool)
            {
                for(var i = 0; i < item.amountToPool; i++)
                {
                    GameObject instance = Instantiate(item.objectToPool) as GameObject;
                    instance.SetActive(false);

                    pooledObjects.Add(instance);
                }
            }
        }

        public GameObject GetPooledObject(string tag)
        {
            for(var i = 0; i < pooledObjects.Count; i++)
            {
                if(!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
                {
                    return pooledObjects[i];
                }
            }

            foreach(ObjectPoolItem item in itemsToPool)
            {
                if(item.objectToPool.tag == tag)
                {
                    if(item.shouldExpand)
                    {
                        GameObject obj = (GameObject)Instantiate(item.objectToPool);
                        obj.SetActive(false);
                        pooledObjects.Add(obj);
                        return obj;
                    }
                }
            }
            return null;
        }
    }
}
