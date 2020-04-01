using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scri_Gem_PickUp : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.SendMessage("AddPoint");
            Destroy(this.gameObject);
            Destroy(transform.parent.gameObject);
        }
    }
}
