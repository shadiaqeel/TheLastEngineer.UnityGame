using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(Enemy))]
public class EnemyPatrol : MonoBehaviour {

	[SerializeField] WaypointController waypointController;
	[SerializeField] float waitTimeMin = 1;
	[SerializeField] float waitTimeMax = 3 ;

	

	PathFinder pathFinder;
	private Enemy m_Enemy;
	public Enemy Enemy
	{
		get
		{ 
			if (m_Enemy == null)
				m_Enemy = GetComponent<Enemy> ();
			return m_Enemy;
		}
	}

	void Start () {
		waypointController.SetNextWaypoint ();
	}

	void Awake () {
		pathFinder = GetComponent<PathFinder> ();
		pathFinder.OnDestinationReached += PathFinder_OnDestinationReached;
		waypointController.OnWaypointChanged += WaypointController_OnWaypointChanged;

		//EnemyPlayer.EnemyHealth.OnDeath += EnemyPlayer_EnemyHealth_OnDeath;
	}


	private void Enemy_OnTargetSelected (TargetTag obj)
	{
		
		pathFinder.SetTarget(obj.transform.position);
	
	}

	/* private void EnemyPlayer_EnemyHealth_OnDeath ()
	{
		pathFinder.Agent.Stop ();
	}
	*/

	private void PathFinder_OnDestinationReached ()
	{
		// assume we are patrolling
		//GameManager.Instance.Timer.Add(waypointController.SetNextWaypoint, Random.Range(waitTimeMin, waitTimeMax));

		//pathFinder.Agent.Stop ();
		pathFinder.Agent.speed=0;
		waypointController.SetNextWaypoint();
	}


	void WaypointController_OnWaypointChanged (Waypoint waypoint)
	{
		
		if (Enemy.EnemyState.CurrentMode == EnemyState.EMode.UNAWARE )
		StartCoroutine(Wait( waypoint)); 

	}


	IEnumerator Wait(Waypoint waypoint){
	  pathFinder.SetTarget (waypoint.transform.position);
     yield return new WaitForSecondsRealtime(Random.Range(waitTimeMin, waitTimeMax)*60);
	  //pathFinder.Agent.Resume ();
	  pathFinder.Agent.speed = 1;
 }
}
