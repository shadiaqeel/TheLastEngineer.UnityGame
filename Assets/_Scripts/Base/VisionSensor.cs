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
	[SerializeField] private float _angle=0;

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
	        col.center = _eyes.transform.localPosition;
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


		/// <summary>
		/// Update is called every frame, if the MonoBehaviour is enabled.
		/// </summary>
		void FixedUpdate()
		{
			col.center = _eyes.transform.localPosition;
	        col.radius = range;

			Debug.DrawRay(_eyes.position,Vector3.forward,Color.cyan);
		}






	#region Scanner 




private void OnTriggerStay(Collider other)
{	
	
	if(state.visionState )
	{
		if (other.tag == targetTag) 
		{



			if (IsInLineOfSight (other.transform.position))
				{
					if(!targets.Contains(other.gameObject))
						targets.Add (other.gameObject);
				}
				else if(targetTag == "Item")
				{
					RemoveItem(other.gameObject);
				}
		
		
	    if( targets.Count >=1 )
	    	{
	
	    	 ClosestTarget();
			
			 if(OnDetectTarget!=null)
	    	 	OnDetectTarget();

			
	    	 }
		}

	     if(targets.Count==0)
	    		if(ownerType == OwnerType.Enemy)
	    			{
	    				enemy.target =  null;
	    			}
	    			else 
	    			{
						
	    				player.item =  null;
	    			}
		
	}	
}

private void OnTriggerExit(Collider other) {
	
	RemoveItem(other.gameObject);

}
public void RemoveItem(GameObject other)
{
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
				player.item =  priorityTarget;

			}
			

		}

		public bool IsInLineOfSight (  Vector3 target)
		{

			Vector3 direction = target - _eyes.position;
			

			//if (Vector3.Angle (_eyes.forward, _eyes.transform.InverseTransformPoint(target)) < fieldOfView / 2) {
			//if ( _eyes.transform.InverseTransformPoint(target).z > 0) {
			if(Vector3.Angle (_eyes.forward, direction)< fieldOfView / 2){
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
			if(_eyes==null || col == null)
			return;

			Gizmos.color = Color.yellow;
			Gizmos.DrawLine (_eyes.position, _eyes.position + GetViewAngle(fieldOfView / 2) * col.radius);
			Gizmos.DrawLine (_eyes.position, _eyes.position + GetViewAngle(-fieldOfView / 2) * col.radius);
		}

		Vector3 GetViewAngle (float angle) 
		{
			float radian = (_angle+angle + _eyes.transform.eulerAngles.y) * Mathf.Deg2Rad;
			return new Vector3 (Mathf.Sin(radian), 0, Mathf.Cos(radian));
		}
	#endregion Gizmos
	}
}