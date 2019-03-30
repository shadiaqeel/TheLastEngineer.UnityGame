using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThelastEngineering.Shared;

namespace ThelastEngineering.Enemy
{
public class EnemyState : CharacterState {

	public enum AIState
	{ Idling , Patrolling , Chasing , Attacking }

	public AIState CurrentAIState = AIState.Patrolling;
	
	
	

	// Use this for initialization
	void Awake () {
        base.init();
		base.VisionState = true;
	}
	
	// Update is called once per frame
	void Update () {
        base.update();

	if(CurrentAIState == AIState.Chasing)
	{
		base.VisionState=false;
	}else
	{
		base.VisionState= true;
	}

	}
}

}
		
	