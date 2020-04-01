using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class scri_GemCollect : MonoBehaviour
    {
        public float speed = 0.2f;

        private Transform target;

        public bool moving;
        // Start is called before the first frame update
        void Start()
        {
            moving = false;
        }

        private void FixedUpdate()
        {
            if (moving == true)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z), step);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
            target = other.transform;
            moving = true;
            }
        }

    }
