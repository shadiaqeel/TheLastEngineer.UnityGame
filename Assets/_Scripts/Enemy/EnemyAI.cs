using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

public enum AIState
{ Idling , Patrolling , Chasing , Attacking }

public enum WanderType { Random, Waypoint};



#region Settings
 


[Header("Enemy Setting")]

public AIState CurrentAIState = AIState.Patrolling;

[SerializeField] private int Damage = 50;

private Animator anim;

private NavMeshAgent agent;

public GameObject target;

private Vector3 lastSeen; 

private VisionSensor vision;

private AudioSource audioSource;

private EnemyState state;

private StateClass playerState;

public float ChaseSpeed = 1f;

[SerializeField] private float animationSmooth;



[Header("Wander Setting")]
public WanderType wanderType = WanderType.Random;

public WaypointsController waypointsController;
private  Waypoint[] waypoints;
private int waypointIndex = 0;
private Vector3 wanderPoint;

public float wanderSpeed = .4f;

public float wanderRadius = 10f;

public float avgWaitingTime = 10;

private bool wait = false;





#endregion Settings





	// Use this for initialization
	void Awake () {
		
		vision = GetComponent<VisionSensor>();
        state = GetComponent<EnemyState>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

		wanderPoint = RandomWanderPoint();
        playerState = null;

        vision.OnDetectTarget+=Attacking_OnDetectTarget;
	}
	
	// Update is called once per frame
	void Update () {
     
     anim.SetBool("IsAlive", state.IsAlive);

    	if(state.IsAlive)
    	{   
            switch(CurrentAIState)
            {
                case AIState.Idling :

                break;


                case AIState.Patrolling:
                Patrol();
                break;


                case AIState.Chasing:

                target = GameObject.FindWithTag("Player");
                agent.SetDestination(target.transform.position);
                break;


                case AIState.Attacking:

                Attacking();

                break;


            }

    
    	}

		
	}


#region Patrol
	void Patrol()
	{
		if(waypointsController != null  )
        {
            wanderType = WanderType.Waypoint;

            if(waypoints == null)
            waypoints=waypointsController.GetWaypoints();

        }
        else
        {
            wanderType = WanderType.Random;

        }



		
    if(!wait)
    {
		//agent.velocity = transform.forward * 0.2f;
        agent.velocity= anim.velocity;

        
        anim.SetFloat("Speed", wanderSpeed, animationSmooth, Time.deltaTime);


        if (wanderType == WanderType.Random)
        {
            if (Vector3.Distance(transform.position, wanderPoint) < 2f)
            {
                //StartCoroutine(WaitingTime ()); 
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
                   // StartCoroutine(WaitingTime ()); 

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
	IEnumerator WaitingTime()
	{        
            wait=true;
			agent.velocity = Vector3.zero;
			//anim.SetBool("Wait",wait);
            yield return new WaitForSecondsRealtime(Random.RandomRange(avgWaitingTime-(avgWaitingTime/2),avgWaitingTime+(avgWaitingTime/2)));
		    wait=false;
			//anim.SetBool("Wait",wait);

           

        }

	
#endregion Patrol


#region Attacking

void Attacking_OnDetectTarget()
{

    Debug.Log (0);
    if (CurrentAIState != AIState.Attacking)
        {
            agent.Stop();
            anim.SetBool("Scream", true);
            CurrentAIState= AIState.Attacking;

        }
    
}
void Attacking()
{

     //Set Destination
        if(target !=null)
        {   
            StopCoroutine(Search());
            anim.SetBool("Miss",false);

            Debug.Log(1);
            agent.SetDestination(target.transform.position);
            lastSeen = target.transform.position;

        }else
        {

            agent.SetDestination(lastSeen);
            Debug.Log(2);
            Debug.Log(lastSeen);

        }
    
    if (!anim.GetBool("Scream"))
    {

        // chase the target
        if(Vector3.Distance(lastSeen,transform.position)<2.0f)
        {
                

            if(target ==null)
            {
                agent.Stop();
                anim.SetBool("Attack", false);
                anim.SetBool("Miss",true);
                
                StartCoroutine(Search());
                
                Debug.Log(3);

            }else
            {
                Attack();
                agent.Stop();
                Debug.Log(4);
            }

        }else{

            agent.Resume();
            anim.SetBool("Attack", false);
            agent.velocity = agent.desiredVelocity;
            anim.SetFloat("Speed",agent.desiredVelocity.magnitude , animationSmooth, Time.deltaTime);
            Debug.Log(5);
        }

        
    }
}

public void Attack()
    {
        anim.SetBool("Attack", true);
        /* if (playerState)
            if (!playerState.IsAlive)
                anim.SetBool("Eat", true);*/
    }

IEnumerator Search()
    {
        
        yield return new WaitForSecondsRealtime(60);
        anim.SetBool("Miss",false);
        if(target==null)
        CurrentAIState = AIState.Patrolling;

    }



#endregion Chase



    void OnAnimatorMove()
    {
        //// Update position based on animation movement using navigation surface height
        Vector3 position = anim.rootPosition;
        position.y = agent.nextPosition.y;
        transform.position = position;
    }


}
