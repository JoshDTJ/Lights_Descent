using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Invector
{
    public class scri_MoveTowards : MonoBehaviour
    {
        private float speed = 0.2f;
        private Transform target;
        public bool moving;
        // Start is called before the first frame update
        void Start()
        {
            moving = false;
        }

        private void FixedUpdate()
        {
            if(moving == true)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {

                var healthController = other.GetComponent<vHealthController>();
                if (healthController.currentHealth < healthController.maxHealth)
                {
                    Debug.Log("Move");
                    moving = true;
                    target = other.transform;
                }
            }
        }

        // Update is called once per frame
        //IEnumerator MoveObj(float time)
        //{
        //    while (moving == true)
        //    {
        //        float step = speed * Time.deltaTime;
        //        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        //        yield return null;
        //    }
        //}
    }
}

