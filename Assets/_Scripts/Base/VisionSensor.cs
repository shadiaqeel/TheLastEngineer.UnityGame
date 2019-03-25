using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThelastEngineering.Enemy;
using ThelastEngineering.Player;


namespace ThelastEngineering.Shared
{
	
	public class VisionSensor : MonoBehaviour {

	public enum  OwnerType
	{
		Player,Enemy
	}



	#region Settings 

	[SerializeField] private OwnerType ownerType;
	[SerializeField] private string targetTag;
	[SerializeField] [Range(0, 360)] float fieldOfView = 90;
	[SerializeField] LayerMask mask;

	[SerializeField]  private float range = 10.0f;
	[SerializeField] private float angle;

	[SerializeField] private Transform _eyes;

	private GameObject priorityTarget;


	EnemyAI enemy;
	UserInput player;
	

	StateClass state;

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
			visionSensor.layer = 9;
			col = visionSensor.AddComponent<SphereCollider>();
	        col.isTrigger = true;
	        col.center += transform.up * 0.9f;
	        col.radius = range;

			


			if(ownerType == OwnerType.Enemy)
			{
			enemy = transform.GetComponent<EnemyAI>();
			}
			else 
			{
			player = transform.GetComponent<UserInput>();
			}


			state = transform.GetComponent<StateClass>();





			targets = new List<GameObject> ();//* */
			//SearchTargets () ;

		}






	#region Scanner 




private void OnTriggerStay(Collider other)
{	
	
	if(state.visionState )
	{
		if (other.tag == targetTag) 
		{
			Debug.Log(other);
			Debug.Log(targets.Contains(other.gameObject));
			Debug.Log("--"+targets.Count);


			if (IsInLineOfSight (other.transform.position))
				{
					if(!targets.Contains(other.gameObject))
						targets.Add (other.gameObject);
				}
		
		
	    if( targets.Count >=1 )
	    	{
	
	    	 ClosestTarget();

			 if(OnDetectTarget!=null)
	    	 	OnDetectTarget();

			 Debug.Log(0);
	    	 }
		}

		
	     if(targets.Count==0)
	    		if(ownerType == OwnerType.Enemy)
	    			{
	    				enemy.target =  null;
	    			}
	    			else 
	    			{
						Debug.Log(1);
	    				player.item =  null;
	    			}
		
	}	
}

private void OnTriggerExit(Collider other) {
	Debug.Log(4);
	if(targets.Contains(other.gameObject))
			targets.Remove (other.gameObject);

}



//------------------------------------------------------------------------------------------ OLD
/* 
		public void SearchTargets () 
		{
			if(state.visionState )
			{

			 targets = new List<GameObject> ();

			Collider[] results = Physics.OverlapSphere (transform.position, range);


			foreach(var target in results)
			{		
				if (target.tag != targetTag) 
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
				if(ownerType == OwnerType.Enemy)
					{
						enemy.target =  null;
					}
					else 
					{
						player.item =  null;

					}

			StartCoroutine(WaitToReScan ()); 
			}

	
		}




		IEnumerator WaitToReScan (){
	     yield return new WaitForSecondsRealtime(0);
		 //SearchTargets (); 
		}

*/
//--------------------------------------------------------------------------------------------


		private void ClosestTarget () {
			float closestTarget = range;
			priorityTarget =targets[0];
			foreach (var possibleTarget in targets) 
			{
				if (Vector3.Distance (transform.position, possibleTarget.transform.position) <= closestTarget)
					priorityTarget = possibleTarget;

			
			}

			if(ownerType == OwnerType.Enemy)
			{
				enemy.target =  priorityTarget;
			}
			else 
			{
				Debug.Log("99 =" + priorityTarget);
				player.item =  priorityTarget;

			}
			

		}

		public bool IsInLineOfSight (  Vector3 target)
		{

			Transform origin = this.transform;
			Vector3 direction = target - _eyes.position;
			if (Vector3.Angle (Vector3.forward, _eyes.transform.InverseTransformPoint(target)) < fieldOfView / 2) {
				float distanceToTarget = Vector3.Distance (_eyes.position, target);
				Debug.DrawRay(_eyes.position , direction.normalized,Color.cyan);
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
}