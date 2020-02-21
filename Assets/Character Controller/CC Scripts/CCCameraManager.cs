using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class CCCameraManager : MonoBehaviour
    {
        public bool lockon;
        public float followSpeed = 9;
        public float mouseSpeed;
        public float controllerSpeed = 7;

        public float lookAngle;
        public float tiltingAngle;
        

        public Transform target;

        public Transform pivot;
        public Transform camTrans;

        float turnSmoothing = .1f;
        public float MinAngle = -35;
        public float MaxAngle = 35;

        float smoothX;
        float smoothY;
        float smoothXVelocity;
        float smoothYVelocity;

        public void Init(Transform t)
        {
            target = t;

            camTrans = Camera.main.transform;
            pivot = camTrans.parent;
        }

       

        //gets input from axis of the mouse as well as controller
        public void Tick(float d)
        {
            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y");

            float c_h = Input.GetAxis("RightAxis X");
            float c_v = Input.GetAxis("RightAxis Y");

            float targetSpeed = mouseSpeed;

            if (c_h != 0 || c_v != 0)
            {
                h = c_h;
                v = c_v;

            }
            //calls on follow target method & handle rotations method
            FollowTarget(d);
            HandleRotations(d, v, h, targetSpeed);
        }
        
        void FollowTarget(float d)
        {
            float speed = d * followSpeed;
            Vector3 targetPosition = Vector3.Lerp(transform.position, target.position, d);
            transform.position = targetPosition;
        }

        //if the turning speed is greater than 0 smoothes the camera
        void HandleRotations(float d, float v, float h, float targetSpeed )
        {
            if(turnSmoothing > 0)
            {
                smoothX = Mathf.SmoothDamp(smoothX, h, ref smoothXVelocity, turnSmoothing);
                smoothY = Mathf.SmoothDamp(smoothY, v, ref smoothYVelocity, turnSmoothing);
            }
            else
            {
                smoothX = h;
                smoothY = v;
            }
            //for future lock on mechanic
            if (lockon)
            {

            }

            lookAngle += smoothX * targetSpeed;
            transform.rotation = Quaternion.Euler(0, lookAngle, 0);

            tiltingAngle -= smoothY * targetSpeed;
            tiltingAngle = Mathf.Clamp(tiltingAngle, MinAngle, MaxAngle);
            pivot.localRotation = Quaternion.Euler(tiltingAngle, 0, 0);
        }

        public static CCCameraManager singleton;
        private void Awake()
        {
            singleton = this;

        }
    }
}