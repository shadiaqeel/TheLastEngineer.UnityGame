using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PathFinder))]
//[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyState))]

public class Enemy : MonoBehaviour {

	PathFinder pathFinder;

	[SerializeField] Scanner Scanner;

	// [SerializeField] Aj settings; 

	TargetTag priorityTarget; 
	List<TargetTag> myTargets;

	public event System.Action<TargetTag> OnTargetSelected;
/* 
	private EnemyHealth m_EnemyHealth;
	public EnemyHealth EnemyHealth 
	{
		get
		{ 
			if (m_EnemyHealth == null)
				m_EnemyHealth = GetComponent<EnemyHealth> ();
			return m_EnemyHealth;
		}
	}

*/
	private EnemyState m_EnemyState;
	public EnemyState EnemyState 
	{
		get
		{ 
			if (m_EnemyState == null)
				m_EnemyState = GetComponent<EnemyState> ();
			return m_EnemyState;
		}
	}


	void Start () {
		pathFinder = GetComponent<PathFinder> ();
		//pathFinder.Agent.speed = settings.WalkSpeed;
		pathFinder.Agent.speed = 1 ;


		Scanner.OnScanReady += Scanner_OnScanReady;
		Scanner_OnScanReady ();

		//EnemyHealth.OnDeath += EnemyHealth_OnDeath;
		EnemyState.OnModeChanged += EnemyState_OnModeChanged;
	}

	private void EnemyState_OnModeChanged (EnemyState.EMode state)
	{
		//pathFinder.Agent.speed = settings.WalkSpeed;
		//pathFinder.Agent.speed = settings.RunSpeed;

		if (state == EnemyState.EMode.AWARE);
			//pathFinder.Agent.speed = 0 ;
	}

	private void EnemyHealth_OnDeath ()
	{
		
	}

	private void Scanner_OnScanReady ()
	{
		if (priorityTarget != null)
			return;

		myTargets = Scanner.ScanForTargets<TargetTag> (); 
		if(myTargets.Count == 0)
		{m_EnemyState.CurrentMode = EnemyState.EMode.UNAWARE;
		priorityTarget = null;
		}
		else if (myTargets.Count == 1)
			priorityTarget = myTargets [0];
		else
			SelectClosestTarget ();

		if (priorityTarget != null) 
		{
			if (OnTargetSelected != null)
				OnTargetSelected (priorityTarget);
		}
	}

	private void SelectClosestTarget () {
		float closestTarget = Scanner.ScanRange;
		foreach (var possibleTarget in myTargets) 
		{
			if (Vector3.Distance (transform.position, possibleTarget.transform.position) < closestTarget)
				priorityTarget = possibleTarget;
		}
	}
	

	void Update () 
	{
		if (priorityTarget != null)
		{

		transform.LookAt (priorityTarget.transform.position);
		pathFinder.SetTarget(priorityTarget.transform.position);
		EnemyState.CurrentMode = EnemyState.EMode.AWARE;

		}

	}
}
