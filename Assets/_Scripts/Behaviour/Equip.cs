using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : StateMachineBehaviour
{
    

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    animator.SetLayerWeight(1,1f);
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {	
        animator.SetLayerWeight(1,0.6f);
    }
}




