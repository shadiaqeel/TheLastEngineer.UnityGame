using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionSensor : MonoBehaviour {



#region Settings 
[SerializeField] [Range(0, 360)] float fieldOfView = 90;
[SerializeField] LayerMask mask;

[SerializeField]  private float range = 10.0f;
[SerializeField] private float angle;




[SerializeField] private Transform _eyes;


private GameObject priorityTarget;
EnemyAI enemy;

List<GameObject> targets;

SphereCollider col;




public event System.Action OnDetectTarget;   





#endregion 


	// Use this for initialization
	void Awake () {
		GameObject visionSensor = new GameObject();
		

	
		visionSensor.name = "VisionSensor";
        visionSensor.transform.position = transform.position;
        visionSensor.transform.parent = transform;
		col = visionSensor.AddComponent<SphereCollider>();
        col.isTrigger = true;
        col.center += transform.up * 0.9f;
        col.radius = range;

		enemy = transform.GetComponent<EnemyAI>();


		SearchTargets () ;

	}


	
	// Update is called once per frame
	void Update () {   }


#region Scanner 



	public void SearchTargets () 
	{

		 targets = new List<GameObject> ();

		Collider[] results = Physics.OverlapSphere (transform.position, range);
		

		foreach(var target in results)
		{		
			if (target.tag != "Player") 
			continue;

			
			if (!IsInLineOfSight (target.transform.position))
				continue;

			targets.Add (target.gameObject);
		}
		

		if(OnDetectTarget!=null & targets.Count >=1)
		{
		 ClosestTarget();
		 OnDetectTarget();
		 }
		 else if(targets.Count==0)
		 enemy.target =null;

		StartCoroutine(WaitToReScan ()); 
			
	}


	IEnumerator WaitToReScan (){
     yield return new WaitForSecondsRealtime(0);
	 SearchTargets (); 
	}


	private void ClosestTarget () {
		float closestTarget = range;
		foreach (var possibleTarget in targets) 
		{
			if (Vector3.Distance (transform.position, possibleTarget.transform.position) < closestTarget)
				enemy.target=possibleTarget;
				//priorityTarget = possibleTarget;
				 
		}
	}
	
	public bool IsInLineOfSight (  Vector3 target)
	{
		
		Transform origin = this.transform;
		Vector3 direction = target - _eyes.position;
		if (Vector3.Angle (Vector3.forward, _eyes.transform.InverseTransformPoint(target)) < fieldOfView / 2) {
			float distanceToTarget = Vector3.Distance (_eyes.position, target);
			Debug.DrawRay(_eyes.position  + origin.forward * .3f, direction.normalized,Color.cyan);
			// something blocking our view?
			if (Physics.Raycast (_eyes.position  /* + _eyes.forward * .3f*/, direction, distanceToTarget, mask)) {
				return false;
			}
			return true;
		}
		return false;
	}



#endregion Scanner



#region Gizmos
		void OnDrawGizmos () 
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine (_eyes.position, _eyes.position + GetViewAngle(fieldOfView / 2) * col.radius);
		Gizmos.DrawLine (_eyes.position, _eyes.position + GetViewAngle(-fieldOfView / 2) * col.radius);
	}

	Vector3 GetViewAngle (float angle) 
	{
		float radian = (angle + _eyes.transform.eulerAngles.y) * Mathf.Deg2Rad;
		return new Vector3 (Mathf.Sin(radian), 0, Mathf.Cos(radian));
	}
#endregion Gizmos
}
