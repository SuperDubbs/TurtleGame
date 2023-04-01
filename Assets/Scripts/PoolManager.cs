using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    private static PoolManager _instance;

    public static PoolManager Instance {get {return _instance;}}

       void Awake()
   {
       if (_instance != null && _instance != this)
       {
           Destroy(this.gameObject);
       }
       else
       {
           _instance = this;
       }
   }

    [System.Serializable]
   public class Pool
   {
       public string name;
       public GameObject prefab;
       public int size;
   }
   public List<Pool> pools;

   public Dictionary<string, Queue<GameObject>> poolDictionary;


    
   void Start()
   {
       poolDictionary = new Dictionary<string, Queue<GameObject>>();

       foreach (Pool pool in pools)
       {
           Queue<GameObject> objectPool = new Queue<GameObject>();
           for (int i = 0; i < pool.size; i++)
           {
               GameObject obj = Instantiate(pool.prefab);
               obj.SetActive(false);
               objectPool.Enqueue(obj);
           }
        poolDictionary.Add(pool.name, objectPool);
       }
   }

   public GameObject GetGameObjectFromPool(string name)
   {
       if (poolDictionary.ContainsKey(name))
       {
            GameObject obj = poolDictionary[name].Dequeue();
            poolDictionary[name].Enqueue(obj);
            return obj;
       }
       Debug.Log("No pool with that name");
       return null;
   }

   public void SpawnFromPool(GameObject obj, Vector3 pos, Vector3 rot)
   {
        obj.transform.position = pos;
        obj.transform.rotation = Quaternion.Euler(rot);
        obj.SetActive(true);

   }
}
