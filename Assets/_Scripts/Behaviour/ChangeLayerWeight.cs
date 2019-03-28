using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayerWeight : StateMachineBehaviour
{

    [SerializeField][Range(0,1f)]public float EnterWeight;
    [SerializeField][Range(0,1f)]public float ExitWeight;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    animator.SetLayerWeight(layerIndex,EnterWeight);
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {	
        animator.SetLayerWeight(layerIndex,ExitWeight);
    }
}
