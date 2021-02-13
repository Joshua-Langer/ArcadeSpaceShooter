using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed = 0f;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.up * speed; //facing y for test projectile instead of on zed.
    }
}
