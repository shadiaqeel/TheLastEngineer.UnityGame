using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Scanner : MonoBehaviour 
{

	[SerializeField] float scanSpeed;
	[SerializeField] [Range(0, 360)] float fieldOfView;
	[SerializeField] LayerMask mask;

	SphereCollider rangeTrigger;

	public float ScanRange {
		get
		{ 
			if (rangeTrigger == null)
				rangeTrigger = GetComponent<SphereCollider> ();
			return rangeTrigger.radius;
		}
	}

	public event System.Action OnScanReady;   
	


	

	void OnDrawGizmos () 
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine (transform.position, transform.position + GetViewAngle(fieldOfView / 2) * GetComponent <SphereCollider>().radius);
		Gizmos.DrawLine (transform.position, transform.position + GetViewAngle(-fieldOfView / 2) * GetComponent <SphereCollider>().radius);
	}

	Vector3 GetViewAngle (float angle) 
	{
		float radian = (angle + transform.eulerAngles.y) * Mathf.Deg2Rad;
		return new Vector3 (Mathf.Sin(radian), 0, Mathf.Cos(radian));
	}

	public List<T> ScanForTargets<T> () 
	{
					Debug.Log("ScanForTargets");

		List<T> targets = new List<T> ();

		Collider[] results = Physics.OverlapSphere (transform.position, ScanRange);

		for (int i = 0; i < results.Length; i++) {
			var target = results [i].transform.GetComponent<T> ();

			if (target == null)
				continue;

			if (!IsInLineOfSight (results[i].transform.position, fieldOfView, mask, Vector3.up))
				continue;
			Debug.Log("**********");
			targets.Add (target);
		}

		StartCoroutine(PrepareScan ()); 
		return targets;
		

		
	}


		IEnumerator PrepareScan(){
     yield return new WaitForSecondsRealtime(1);
	 if (OnScanReady != null)
		OnScanReady();
 }

bool IsInLineOfSight (  Vector3 target, float fieldOfView, LayerMask collisionMask, Vector3 offset)
		{
			Debug.Log("IsInLineOfSight = "+ target  );


			Transform origin = this.transform;
			Vector3 direction = target - origin.position;

			if (Vector3.Angle (origin.forward, direction.normalized) < fieldOfView / 2) {
				float distanceToTarget = Vector3.Distance (origin.position, target);

				// something blocking our view?
				if (Physics.Raycast (origin.position + offset + origin.forward * .3f, direction.normalized, distanceToTarget, collisionMask)) {
					return false;
				}

				return true;
			}

			return false;
		}




	
}
