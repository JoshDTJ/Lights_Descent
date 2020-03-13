using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

public class GargoyleIdle : StateBehaviour
{
    Rigidbody model_Gargoyle;

    void OnEnable()
    {
        model_Gargoyle = GetComponent<Rigidbody>();
        Invoke("StartWalking",5f);
        model_Gargoyle.velocity = Vector3.zero;
    }

    void StartWalking()
    {
        SendEvent("GargoyleStartIdle");
        //SendEvent(GargoyleStartRun);
    }
}
