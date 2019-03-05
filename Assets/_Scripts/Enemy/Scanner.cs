using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class Scanner : MonoBehaviour {

[SerializeField] [Range(0, 360)] float fieldOfView = 90;
[SerializeField] LayerMask mask;

SphereCollider rangeTrigger;



	public event System.Action OnScanReady;   


	void Start () {}
	
	// Update is called once per frame
	void Update () {}









	public float ScanRange {
		get
		{ 
			if (rangeTrigger == null)
				rangeTrigger = GetComponent<SphereCollider> ();
			return rangeTrigger.radius;
		}
	}






	public List<T> ScanForTargets<T> () 
	{

		List<T> targets = new List<T> ();

		Collider[] results = Physics.OverlapSphere (transform.position, ScanRange);

		for (int i = 0; i < results.Length; i++) {
			var target = results [i].transform.GetComponent<T> ();

			if (target == null)
				continue;

			if (!IsInLineOfSight (results[i].transform.position, Vector3.up))
				continue;
			targets.Add (target);
		}

		StartCoroutine(WaitToReScan ()); 
		return targets;
		

		
	}


	IEnumerator WaitToReScan (){
     yield return new WaitForSecondsRealtime(1);
	 if (OnScanReady != null)
		OnScanReady();
	}



	public bool IsInLineOfSight (  Vector3 target, Vector3 offset)
		{
			
			Transform origin = this.transform;
			Vector3 direction = target - origin.position;

			if (Vector3.Angle (Vector3.forward, transform.InverseTransformPoint(target)) < fieldOfView / 2) {
				float distanceToTarget = Vector3.Distance (origin.position, target);

				Debug.DrawRay(origin.position + offset + origin.forward * .3f, direction.normalized,Color.cyan);
				// something blocking our view?
				if (Physics.Raycast (origin.position + offset + origin.forward * .3f, direction.normalized, distanceToTarget, mask)) {
					return false;
				}


				return true;
			}

			return false;
		}












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
}
