/*
For player projectiles use only, will not work with anything else.
*/

using System.Collections;
using System.Collections.Generic;
using ArcadeShooter.Managers;
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
            if(other.tag == "Obstacle"){GameManager.AddToScore(50);}
            else if(other.tag == "Enemy"){GameManager.AddToScore(100);}
        }
    }
}
