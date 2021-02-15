using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcadeShooter.CameraSystem{
    public class CameraFollow : MonoBehaviour
    {
        public Transform Target;
        public Transform camTransform;
        public Vector3 Offset;
        public float smoothTime = 0.3f;

        private Vector3 velocity = Vector3.zero;

        private void Start()
        {
            Offset = camTransform.position - Target.position;
        }   

        private void LateUpdate()
        {
            Vector3 targetPosition = Target.position + Offset;
            camTransform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            //transform.LookAt(Target);
        }
    }
}
