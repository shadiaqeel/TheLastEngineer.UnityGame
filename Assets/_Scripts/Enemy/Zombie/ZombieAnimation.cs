using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

public class ZombieAnimation : MonoBehaviour {

	[SerializeField] Animator anim;

	private Enemy enemy;
	private NavMeshAgent agent;
	private EnemySetting  enemySetting;
	
	private Transform Transform; 



	



	// Use this for initialization
	void Start () {


		anim = GetComponent<Animator>();
		enemy = GetComponent<Enemy> ();
		agent = GetComponent<NavMeshAgent>();
		
	}
	
	// Update is called once per frame
	void Update () {

		anim.SetBool ("IsWalking",agent.speed!=0);
		

		anim.SetBool ("Aware", enemy.IsAware);




		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Wait"))
		{
			anim.applyRootMotion=true;
		}
		else
		{	anim.applyRootMotion=false;} 



	if( enemy.IsAware  &&  !enemy.Target.Miss )
	{

		anim.SetBool("IsAttacking", agent.remainingDistance < 2.5 );	
		
	}
	
		
	
	}
}


