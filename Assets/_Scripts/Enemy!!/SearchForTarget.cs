using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

public class SearchForTarget : MonoBehaviour {


private Enemy enemy;
public Scanner scanner;
private NavMeshAgent agent;

private bool IsMiss = false;

private bool IsInLineOfSight ;

private TargetTag  priorityTarget; 


public float chaseSpeed = 1f;

public float searchAfterMissTime = 10;
List<TargetTag> myTargets;


	// Use this for initialization
	void Start () {
		
		enemy = GetComponent<Enemy>();
		scanner = GetComponentInChildren<Scanner>();
		agent = GetComponent<NavMeshAgent>();

		scanner.OnScanReady += Scanner_OnScanReady;
		Scanner_OnScanReady();

	}
	
	// Update is called once per frame
	void Update () {


	if(priorityTarget != null){

		IsInLineOfSight = scanner.IsInLineOfSight(priorityTarget.transform.position);


		 if(IsInLineOfSight)
		 {	
			 Debug.Log(2);
			 IsMiss =false;
			 enemy.IsAware  = true;
			agent.SetDestination(priorityTarget.transform.position);
			//agent.speed = chaseSpeed;
			if(Vector3.Distance(enemy.transform.position, priorityTarget.transform.position) < 2.5)
			{
			enemy.CurrentMove = Enemy.EnemyMoveState.STOP;
			}else {
				enemy.CurrentMove = Enemy.EnemyMoveState.RUN;
			}

		 }
		 else 
		 {
			 if(!(agent.destination == enemy.transform.position))
			 { 
				 Debug.Log(3);
				 IsMiss = true; 
				 enemy.CurrentMove = Enemy.EnemyMoveState.RUN;
				Scanner_OnScanReady ();
			 }

			 else
			 {
				 Debug.Log(4);
				StartCoroutine(SearchAfterMiss()); 
				
			 }

		 }


		 if(Vector3.Distance(enemy.transform.position,priorityTarget.transform.position)<5){	
			agent.Stop();
			Vector3 TargetDir = priorityTarget.transform.position - enemy.transform.position  ;
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(TargetDir), Time.time * .5f);
			agent.Resume();
		 }

	}
	else
	{
	 Debug.Log(5);
		IsMiss =false;
		enemy.IsAware  = false;
		Scanner_OnScanReady ();

	}


}




#region Methodes
	private void Scanner_OnScanReady ()
	{
		if (priorityTarget != null && IsMiss==false)
			return;

		myTargets = scanner.ScanForTargets<TargetTag> (); 
		
		if(myTargets.Count == 0)
		{//enemy.IsAware = false;
		//priorityTarget = null;
		}
		else if (myTargets.Count == 1)
			{
				priorityTarget = myTargets [0];
			}
		else
			{   SelectClosestTarget ();}

		
	}


	private void SelectClosestTarget () {
		float closestTarget = scanner.ScanRange;
		foreach (var possibleTarget in myTargets) 
		{
			if (Vector3.Distance (transform.position, possibleTarget.transform.position) < closestTarget)
				priorityTarget = possibleTarget;
		}
	}






	IEnumerator SearchAfterMiss(){
            
            //agent.speed = 0;
			enemy.CurrentMove = Enemy.EnemyMoveState.STOP;
            yield return new WaitForSecondsRealtime(searchAfterMissTime);
			if(IsMiss)
			{
			enemy.CurrentMove = Enemy.EnemyMoveState.WALK;
			IsMiss = false;
			enemy.IsAware  = false;
			priorityTarget = null;
			}

        }
#endregion Methodes



#region Properties 

public bool Miss
    {
        get{
        return IsMiss; 
		}
        
        
    }

#endregion Properties 





}

	