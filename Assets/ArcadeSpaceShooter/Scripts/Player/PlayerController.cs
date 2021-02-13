using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcadeShooter.Player{
    [System.Serializable]
    public class Boundary
    {
        public float xMin, xMax, zMin, zMax;
    }

    public class PlayerController : MonoBehaviour
    {
        public float speed = 0f;
        public float tilt = 0f;
        public Boundary boundary;

        public GameObject testProjectile;
        public Transform projectileSpawn;
        public float fireRate = 0f;

        private float nextFire = 0f;

        void FixedUpdate()
        {
            Controls();
        }

        void Update()
        {
            FiringControl();
        }

        void Controls()
        {
            var moveHorizontal = Input.GetAxis("Horizontal");
            var moveVertical = Input.GetAxis("Vertical");

            var movement = new Vector3 (moveHorizontal, 0, moveVertical);
            GetComponent<Rigidbody>().velocity = movement * speed;

            GetComponent<Rigidbody>().position = new Vector3(
                Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
                0,
                Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
            );
    //TODO: Figure out the tilt as it's not moving correctly, instead of tilting, its moving the front of the ship to the left or right at about 30 degrees.
            GetComponent<Rigidbody>().rotation = Quaternion.Euler(90, 0, GetComponent<Rigidbody>().velocity.x * -tilt);
        }

        void FiringControl()
        {
            if(Input.GetButton("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(testProjectile, projectileSpawn.position, projectileSpawn.rotation);
                //Play Audio FX
            }
        }
    }
}
