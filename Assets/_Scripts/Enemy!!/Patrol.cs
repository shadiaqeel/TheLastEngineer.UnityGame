using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//using UnityStandardAssets.Characters.FirstPerson;
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

public class Patrol : MonoBehaviour {

public enum WanderType { Random, Waypoint};

public WanderType wanderType = WanderType.Random;
 
 
  
    public WaypointsController waypointsController;
	private  Waypoint[] waypoints;
	private int waypointIndex = 0;
	private Vector3 wanderPoint;

    private NavMeshAgent agent;
    private Enemy enemy ;


    private bool NotWait = true;


    public float wanderSpeed = 1f;

	public float wanderRadius = 10f;

    public float avgWaitingTime = 10;

	




	// Use this for initialization
	void Start () {

		agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
		wanderPoint = RandomWanderPoint();
        
		
	}


    void Update () {

 if(waypointsController != null )
        {
            wanderType = WanderType.Waypoint;
            waypoints=waypointsController.GetWaypoints();

        }
        else
        {
            wanderType = WanderType.Random;

        }

    }
	
	

#region Methode
	public void Wander()
    {

    if(NotWait)
    {
		

        if (wanderType == WanderType.Random)
        {
            if (Vector3.Distance(transform.position, wanderPoint) < 2f)
            {
                 StartCoroutine(WaitingTime ()); 
                wanderPoint = RandomWanderPoint();
            }
            else
            {

                agent.SetDestination(wanderPoint);


            }
        }
        else
        {
            //Waypoint wandering
            if (waypoints.Length >= 2)
            {
                if (Vector3.Distance(waypoints[waypointIndex].transform.position, transform.position) < 2f)
                {
                     StartCoroutine(WaitingTime ()); 

                    if (waypointIndex == waypoints.Length - 1)
                    {
                        waypointIndex = 0;
                    }
                    else
                    {
                        waypointIndex++;
                    }
                }
                else
                {
                    agent.SetDestination(waypoints[waypointIndex].transform.position);
                
                }
            } else
            {
                Debug.LogWarning("Please assign more than 1 waypoint to the AI: " + gameObject.name);
            }

        }
     }
    }






    public Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);
        return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
    }

    	IEnumerator WaitingTime(){
            
            NotWait=false;
            //agent.speed = 0;
            enemy.CurrentMove = Enemy.EnemyMoveState.STOP;
            
            yield return new WaitForSecondsRealtime(Random.RandomRange(avgWaitingTime-(avgWaitingTime/2),avgWaitingTime+(avgWaitingTime/2)));
            NotWait=true;
            //agent.speed = wanderSpeed;  
            enemy.CurrentMove = Enemy.EnemyMoveState.WALK;

           

        }

#endregion

}

