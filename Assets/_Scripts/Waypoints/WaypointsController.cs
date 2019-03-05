using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsController : MonoBehaviour {

	Waypoint[] waypoints;

	

	void Awake () {
		waypoints = GetWaypoints();
	}


	public Waypoint[] GetWaypoints() {
		return GetComponentsInChildren<Waypoint> ();
	}
	




	void OnDrawGizmos () {
		Gizmos.color = Color.blue;

		Vector3 previousWaypoint = Vector3.zero;
		foreach (var waypoint in GetWaypoints()) 
		{
			Vector3 waypointPosition = waypoint.transform.position;
			Gizmos.DrawWireSphere (waypointPosition, .2f);
			if (previousWaypoint != Vector3.zero)
				Gizmos.DrawLine (previousWaypoint, waypointPosition);
			previousWaypoint = waypointPosition;
		}
	}
}
