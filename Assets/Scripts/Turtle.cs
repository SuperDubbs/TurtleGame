using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    public float maxSpeed = 100;
    public float accelerationSpeed = 20;
    public float size = 1;
    public Vector3 movedirection;
    public bool carryingShip;
    public Ship shipScript;
    public float pickUpRange = 1f;
    public Transform risenPosition; 
    public Rigidbody rb;

    Vector3 normalizeXZ(Vector3 v)
    {
        Vector3 normalizedVector = new Vector3(v.x, 0f, v.z);
        return new Vector3(normalizedVector.x, v.y, normalizedVector.z);
    }

    void pickupBoat()
    {
        carryingShip = true;
        foreach (GameObject ship in PoolManager.Instance.poolDictionary["Ship"])
        {
            if (ship.activeSelf == true && Vector3.Distance(transform.position, ship.transform.position) < pickUpRange)
            {
                ship.transform.SetParent(transform);
                shipScript = GetComponentInChildren<Ship>();
                shipScript.pickedUp = true;
                return; 
            }
        }
       
    }

    void dropBoat()
    {
        carryingShip = false;
        if(shipScript != null)
        {
            shipScript.pickedUp = false;
            shipScript.transform.SetParent(null);
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        movedirection.x = Input.GetAxis("Horizontal");
        movedirection.z = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (carryingShip == false)
            {
                pickupBoat();
                movedirection.y = 0.3f;
            }
            else if (carryingShip == true)
            {
                dropBoat();
                movedirection.y = -0.3f;
            }
        }
       if (transform.position.y <-1.3f)
       {
           movedirection.y = 0f;
           transform.position = new Vector3(transform.position.x,-1.3f,transform.position.z);
       }
       else if(transform.position.y > -0.7f)
       {
           movedirection.y = 0f;
           transform.position = new Vector3(transform.position.x,-0.7f,transform.position.z);
       }
       rb.MovePosition(transform.position +(normalizeXZ(movedirection) *Time.deltaTime* maxSpeed));
    }
}
