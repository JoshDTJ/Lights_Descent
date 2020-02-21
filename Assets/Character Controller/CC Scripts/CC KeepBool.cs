using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepBool : StateMachineBehaviour
{
    //for as long as Onstate update runs this will prevent the player from moving in an attack 

    public string boolName;
    public bool status;
   

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 

        animator.SetBool(boolName, status);

    }

    
}
