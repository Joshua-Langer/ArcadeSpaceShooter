using System.Collections;
using System.Collections.Generic;
using ArcadeShooter.Managers;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    Rigidbody rigidbody;
    float speed;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        speed = Random.Range(1f, 6f);
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

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(other.gameObject);
            GameManager.GameOver(true);
        }
    }
}
