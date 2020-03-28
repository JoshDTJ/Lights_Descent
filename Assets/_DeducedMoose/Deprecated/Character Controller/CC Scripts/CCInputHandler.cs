using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA {
    public class CCInputHandler : MonoBehaviour
    {
        float Vertical;
        float Horizontal;

        public Rigidbody rigid;

        CCStateManager states;
        CCCameraManager camManager;

        float delta;

        //calls the state manager and cam manager scripts
        void Start()
        {
            states = GetComponent<CCStateManager>();
            states.Init();

            camManager = CCCameraManager.singleton;
            camManager.Init(this.transform);
        }

        //checks for input every update
        void FixedUpdate()
        {
            delta = Time.fixedDeltaTime;
            GetInput();
            UpdateStates();
            states.FixedTick(delta);
            camManager.Tick(delta);

        }
        
        void Update()
        {
            delta = Time.deltaTime;
            states.Tick(delta);
        
            
        }
        
        void GetInput()
        {
            //retrieve input on the listed axises
            Vertical = Input.GetAxis("Vertical");
            Horizontal = Input.GetAxis("Horizontal");
        }
        void UpdateStates()
        {
            states.Horizontal = Horizontal;
            states.Vertical = Vertical;

            //movement based on camera angle makes moving diagonally with a controller not a jittery mess
            Vector3 v = states.Vertical * camManager.transform.forward;
            Vector3 h = Horizontal * camManager.transform.right;
            states.moveDir = (v + h).normalized;
            float m = Mathf.Abs(Horizontal) + Mathf.Abs(Vertical);
            states.MoveAmount = Mathf.Clamp01(m);

            states.FixedTick(Time.deltaTime);
        }
    }
}
