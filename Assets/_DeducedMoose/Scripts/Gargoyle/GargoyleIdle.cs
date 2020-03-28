using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

public class GargoyleIdle : StateBehaviour
{
    Rigidbody model_Gargoyle;
    public float idleInterval = 5;

    void OnEnable()
    {
        model_Gargoyle = GetComponent<Rigidbody>();
        Invoke("StartWalking", 5f);
        model_Gargoyle.velocity = Vector3.zero;
        idleInterval = 5;
    }

    void StartWalking()
    {
        
        SendEvent("GargoyleStartWalk");
        /*idleInterval -= Time.deltaTime;

        if (idleInterval < 0)
        {
          
            SendEvent("GargoyleStartWalk");
        }*/

    }
}
