using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

public class Enemy : MonoBehaviour {

    private enum EnemyState { AWARE, UNAWARE};
    public enum EnemyMoveState { STOP, WALK , RUN};
    private EnemyState CurrentState = EnemyState.UNAWARE ;

    private EnemyMoveState CurrentMoveState = EnemyMoveState.WALK;

    private Patrol patrol;

    private NavMeshAgent agent;


    public SearchForTarget  Target;

    [SerializeField] EnemySetting enemySetting;

    


    
    public void Start()
    {
        
        patrol = GetComponent<Patrol>();
        Target = GetComponent<SearchForTarget>();
        agent = GetComponent<NavMeshAgent>();


    }
    public void Update()
    {

        Debug.Log(agent.destination);
        if (!IsAware)
        {
                patrol.Wander();
        }
    
        
    }















#region Properties 

    public bool IsAware
    {
        get{
        if(CurrentState == EnemyState.AWARE)
        return true;
        else 
        return false;}
        
        set{
            if(value)
            {CurrentState = EnemyState.AWARE;}
            else{CurrentState = EnemyState.UNAWARE;}
            
        }


    }

    public EnemyMoveState CurrentMove
    {
        get
        {
            return CurrentMoveState;
        }

        set
        {

            CurrentMoveState = value;

            if(CurrentMoveState==EnemyMoveState.STOP)
            {
                agent.speed=0;
            }
            else if(CurrentMoveState==EnemyMoveState.WALK){
                
                agent.speed = 0.4f;
                //agent.speed = enemySetting.WanderSpeed;
            }
            else{
               
               agent.speed = 1.2f;
               // agent.speed = enemySetting.ChaseSpeed;

                }

            
            
        }
    }

  

#endregion Properties

}



 

