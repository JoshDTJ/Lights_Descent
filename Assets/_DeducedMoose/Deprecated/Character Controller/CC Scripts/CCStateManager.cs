using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class CCStateManager : MonoBehaviour
    {
        [Header("Init")]
        public GameObject ActiveModel;
        

        [Header("Inputs")]
        public float Vertical;
        public float Horizontal;
        public float MoveAmount;
        public Vector3 moveDir;
        public float toGround = 0.5f;


        [Header("Stats")]
        public float moveSpeed = 5;
        public float runSpeed = 7;
        public float rotateSpeed = 5;

        [Header("Stats")]
        public bool run;
        public bool onGround;


        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public Rigidbody rigid;
        [HideInInspector]
        public float delta;
        [HideInInspector]
        public LayerMask ignoreLayers;
        
        public void Init()
        {
            SetupAnimator();
            rigid = GetComponent<Rigidbody>();
            rigid.angularDrag = 1;
            rigid.drag = 4;
            rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            gameObject.layer = 8;
            ignoreLayers = ~(1 << 9);
        }
        //sets up animator by looking for the active model
        void SetupAnimator()
        {
            if (ActiveModel == null)
            {
                anim = GetComponentInChildren<Animator>();
                if (anim == null)
                {
                    Debug.Log("No model found");
                }
                else
                {
                    ActiveModel = anim.gameObject;
                }
            }

            if (anim == null)
                anim = ActiveModel.GetComponent<Animator>();
        }

        public void FixedTick(float d)
        {
            delta = d;

            rigid.drag = (MoveAmount > 0 || onGround == false) ? 0 : 4;

            if (MoveAmount > 0)
            {
                rigid.drag = 0;
            }
            else
                rigid.drag = 4;

            float targetSpeed = moveSpeed;
            if (run)
                targetSpeed = runSpeed;

           
            if(onGround)
                rigid.velocity = moveDir * (targetSpeed * MoveAmount);

            Vector3 targetDir = moveDir;
                targetDir.y = 0;
            if (targetDir == Vector3.zero)
                targetDir = transform.forward;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, delta * MoveAmount * rotateSpeed);
            transform.rotation = targetRotation;

            HandleMovementAnimations();

        }

        public void Tick(float d)
        {
            delta = d;
            onGround = OnGround();
        }
        
        //plays animation if moving "vertical" (which is actually just forward)
        void HandleMovementAnimations()
        {
            anim.SetFloat("Vertical", MoveAmount, 0.4f, delta);
        }
        public bool OnGround()
        {
            bool r = false;

            Vector3 origin = transform.position + Vector3.up * toGround;
            Vector3 dir = -Vector3.up;
            float dis = toGround + 0.3f;
            RaycastHit hit;
            Debug.DrawRay(origin, dir * dis);
            if (Physics.Raycast(origin, dir, out hit, dis, ignoreLayers))
            {
                r = true;
                Vector3 targetPosition = hit.point;
                transform.position = targetPosition;
            }

            return r;
        }
    }
}