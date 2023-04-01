using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float moveSpeed = 5;
    public bool pickedUp = false;

    public Rigidbody rb;

    public GameObject destroyedObj;
    public LayerMask waterLayer;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != waterLayer)
        {
            Instantiate(destroyedObj, transform.position, transform.rotation);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    Vector3 AngleToUnitCircle(float angle)
    {
        angle = (angle - 90f) *-1f;
        float radians = angle *Mathf.Deg2Rad;
        float x = Mathf.Cos(radians);
        float y = Mathf.Sin(radians);
        Vector3 pos = new Vector3(x, 0f, y);
        return pos;
    }
    void FixedUpdate()
    {
        Vector3 moveDirection = AngleToUnitCircle(transform.rotation.eulerAngles.y);
        if (!pickedUp)
        {
            if (transform.position.y > 0f)
            {
                moveDirection.y = -1.5f;
            }
            else if(transform.position.y < 0f)
            {
                moveDirection.y = 0f;
                transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            }
            rb.MovePosition(transform.position +(moveDirection * moveSpeed * Time.deltaTime));
        }

    }
}
