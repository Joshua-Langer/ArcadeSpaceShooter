using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    Rigidbody rigidbody;
    float speed;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        speed = Random.Range(1f, 6f);
        Debug.Log("Asteroid speed is " + speed);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        ObstacleMovement();
    }

    void ObstacleMovement()
    {
        Vector3 fall = Vector3.back;
        rigidbody.velocity = fall * speed;
    }
}
