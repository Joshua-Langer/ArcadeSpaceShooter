using UnityEngine;

namespace ArcadeShooter.Player{
    public class PlayerInput : MonoBehaviour
    {
        public bool IsFiring => Input.GetButton("Fire1");

        public Vector3 GetCameraSpaceInputDirection(Camera camera)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 inputDirection = new Vector3(h, 0, v);

            if(camera == null)
            {
                return inputDirection;
            }

            Vector3 cameraRight = camera.transform.right;
            Vector3 cameraForward = camera.transform.forward;

            return cameraRight * inputDirection.x + cameraForward * inputDirection.z;
        }

    }
}