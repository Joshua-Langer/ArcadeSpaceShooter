using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcadeShooter.Player{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] Transform shipTransform;
        [SerializeField] float moveSpeed = 3f;
        [Range(0.05f, 0.3f)] [SerializeField] float turnSpeed = 0.1f;

        Rigidbody playerRigidbody;

        void Awake()
        {
            playerRigidbody = GetComponent<Rigidbody>();
        }

        void Start()
        {
            if(shipTransform != null)
            {
                shipTransform.parent = null;
            }
        }

        public void MovePlayer(Vector3 direction)
        {
            Vector3 moveDirection = new Vector3(direction.x, 0f, direction.z);
            moveDirection = moveDirection.normalized * moveSpeed * Time.deltaTime;
            playerRigidbody.MovePosition(transform.position + moveDirection);
        }

        private Quaternion GetRotationToTarget(Transform xform, Vector3 targetPosition)
        {
            // get a normalized vector to the target on the xz-plane
            Vector3 direction = targetPosition - xform.position;
            direction.y = 0f;
            direction.Normalize();

            // convert Vector3 to Quaternion and return
            return Quaternion.LookRotation(direction);
        }

        // returns correction rotation to face the mouse pointer (using y rotation)
        private Quaternion GetRotationToMouse(Transform xform, Camera cam)
        {
            if (cam == null)
            {
                Debug.Log("PLAYERMOVER GetRotationToMouse: no camera");
                return xform.rotation;
            }

            // use Raycast and GroundLayer mask to calculate mouse position in world space
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                return GetRotationToTarget(xform, hit.point);
            }

            return xform.rotation;
        }


    }
}
