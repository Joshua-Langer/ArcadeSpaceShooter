using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Obstacle" || other.tag == "Enemy")
        {
            //TODO: Create Explosion
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
