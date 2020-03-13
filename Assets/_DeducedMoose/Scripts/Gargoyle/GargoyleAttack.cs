using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviourMachine;

public class GargoyleAttack : StateBehaviour
{
    public GameObjectVar ThePlayer;
    private NavMeshAgent TheGar;
     NavMeshPath GarPath; 
    public float timerStartTime;
    public float timerInterval = 3;

   void OnEnable()
    {
        blackboard.GetGameObjectVar("PlayerKill").Value = FindObjectOfType<ThirdPersonCharacterController>().gameObject;
        timerStartTime = Time.time;
    }
   void Awake()
    {
        TheGar = GetComponent<NavMeshAgent>();
        GarPath = new NavMeshPath();
    }
    // Update is called once per frame
    void Update()
    {
        GarToNextPoint();
    }
    void GarToNextPoint()
    {
        TheGar.CalculatePath(blackboard.GetGameObjectVar("PlayerKill").Value.transform.position, GarPath);
        TheGar.SetPath(GarPath);
        if (Vector3.Distance(transform.position, blackboard.GetGameObjectVar("PlayerKill").Value.transform.position) < 2)
        {
            SendEvent("GargoyleStartAttack");
        }
        else if(timerStartTime + timerInterval < Time.time)
        {
            SendEvent("GargoyleStartIdle");
        }
    }
}
