using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
public class GargoyleRun : StateBehaviour
{
    public GameObjectVar waypointParent;
    public FloatVar garSpeed;
    public float garHealth;

    public Vector3 pointToGarTo;
    public float runInterval;

 void OnEnable()
    {
        waypointParent = GetComponent<Blackboard>().GetGameObjectVar("wayPointParent");
        garSpeed = GetComponent<Blackboard>().GetFloatVar("garSpeed");

        pointToGarTo = waypointParent.Value.transform.GetChild(Random.Range(0, waypointParent.transform.childCount)).position;
        runInterval = 5;
    }

  void Update()
    {
        GarToPoint();
    }

    void GarToPoint()
    {
        runInterval -= Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pointToGarTo - transform.position, garSpeed.Value * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.position, pointToGarTo - transform.position.normalized, 5, 0), Vector3.up);

        if(Vector3.Distance(transform.position,pointToGarTo - transform.position) <1f)
            if(runInterval < 0)
            {
               // runInterval = 0;
                SendEvent("GargoyleStartAttack");
            }
    }
}
