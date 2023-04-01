using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   
   private static GameManager _instance;
   public static GameManager Instance {get {return _instance;} }
   public Transform player;
   public float spawnRadius = 20f;
   public float despawnRadius = 40f;
 
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
   void SpawnShip()
   {
      GameObject obj = PoolManager.Instance.GetGameObjectFromPool("Ship");
      if(Vector3.Distance(player.position, obj.transform.position) > despawnRadius || obj.activeSelf == false)
      {
            Vector2 xzpos = Random.insideUnitCircle;
            Vector3 xyzpos = new Vector3(xzpos[0], 0 , xzpos[1]);
            Vector3 pos = player.transform.position + (xyzpos).normalized*spawnRadius;
            pos.y = 0f;
            Vector3 rot = new Vector3(0f,Random.Range(0f,360f),0f);
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            PoolManager.Instance.SpawnFromPool(obj, pos, rot);
      }
   }

   void Update()
   {
       SpawnShip();
   }

}
