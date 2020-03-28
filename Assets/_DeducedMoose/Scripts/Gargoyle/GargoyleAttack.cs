using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviourMachine;

public class GargoyleAttack : StateBehaviour
{
    public GameObjectVar ThePlayer;
    public int playerHealth = 100;

    public float garlookRdius;
    public int damagePlayer;
    private NavMeshAgent TheGar;

    //public BoolVar garAttackPlayer;
    NavMeshPath GarPath;
    public float timerStartTime;
    public float timerInterval = 3;

    private void OnEnable()
    {
        blackboard.GetGameObjectVar("PlayerKill").Value = FindObjectOfType<ThirdPersonCharacterController>().gameObject;
        timerStartTime = Time.time;
    }
    void Start()
    {
        timerInterval = 3;
        TheGar = GetComponent<NavMeshAgent>();
        GarPath = new NavMeshPath();
    }
    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, transform.position);
        if (distance <= garlookRdius)
        {
            GarToNextPoint();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            damagePlayer -= playerHealth;
            print("Idamagedtheplayer");
        }
    }
    void GarToNextPoint()
    {
        timerInterval -= Time.deltaTime;
        TheGar.CalculatePath(blackboard.GetGameObjectVar("PlayerKill").Value.transform.position, GarPath);
        TheGar.SetPath(GarPath);

        //1st attempt to make stat transition back to idle 
       /* if(timerInterval == 0)
        {
            SendEvent("GargoyleStartIdle");
        }*/ 

        //2nd attempt to make state transition back to idle
        if (Vector3.Distance(transform.position, blackboard.GetGameObjectVar("PlayerKill").Value.transform.position) < 2)
        {
            timerInterval = 0;
            SendEvent("GargoyleStartIdle");
        }
    }

}
