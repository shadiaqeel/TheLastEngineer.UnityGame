using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimation : MonoBehaviour {

	[SerializeField] Animator anim;

	PathFinder pathFinder;
	Enemy enemy;
	Transform TransformParent; 
	Transform Transform; 

	TargetTag  Target;

	int WaitToPrepare;

	



	// Use this for initialization
	void Start () {


		anim = GetComponent<Animator>();
		pathFinder = GetComponent<PathFinder> ();
		enemy = GetComponent<Enemy> ();
		enemy.OnTargetSelected += Enemy_OnTargetSelected;

		WaitToPrepare = 2;


		
	}
	
	// Update is called once per frame
	void Update () {


		anim.SetBool ("IsWalking",pathFinder.Agent.speed != 0);

		anim.SetBool ("Aware", enemy.EnemyState.CurrentMode == EnemyState.EMode.AWARE);

		if(!anim.GetCurrentAnimatorStateInfo(0).IsName("walk"))
		{
			anim.SetFloat("Speed",1);

		}

		if(anim.GetCurrentAnimatorStateInfo(0).IsName("wait"))
		{
			anim.applyRootMotion=true;
		}
		else
		{anim.applyRootMotion=false;} 



	if(Target != null)
	{

		
		anim.SetBool("IsAttacking", (enemy.transform.position-Target.transform.position ).magnitude < 3.0 );
				
		
	}
	
		/*Example :


		float velocity = ((transform.position - lastPosition).magnitude) / Time.deltaTime;
		lastPosition = transform.position;
		animator.SetFloat ("Vertical", velocity / pathFinder.Agent.speed); 
		
		
		*/
	}

	private void Enemy_OnTargetSelected (TargetTag target)
	{

	Target = target;	
	StartCoroutine(Prepare()); 
	}

	IEnumerator Prepare(){
     yield return new WaitForSecondsRealtime(WaitToPrepare);
	 pathFinder.Agent.speed = 2;
 }


}


