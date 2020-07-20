using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_SimplePatrol : MonoBehaviour
{

    //Waiting at Node
    [SerializeField]
    bool patrolWaiting;

    //Time waiting at Node
    [SerializeField]
    float totalWaitTime = 3f;

    //Chance that NPC will alter direction
    [SerializeField]
    float probabilitySwitch = .2f;

    //Nodes
    [SerializeField]
    List<Waypoint> patrolPoints;

    //Base Behavior
    NavMeshAgent navMeshAgent;
    int currentPatrolPoint;
    bool travelling;
    bool waiting;
    bool patrolForward;
    float waitTimer;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        if(navMeshAgent == null)
        {
            Debug.LogError("NO NAV MESH ON THIS COMPONENT YOU TWIT!!!: " + gameObject.name);
        }
        else
        {
            if(patrolPoints != null && patrolPoints.Count >= 2)
            {
                currentPatrolPoint = 0;
                SetDestination();
            }
            else
            {
                Debug.Log("Not Enough Patrol Points to patrol you TWIT!!");
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Check if close to destination
        if(travelling && navMeshAgent.remainingDistance <= 1.0f)
        {
            travelling = false;

            //Waiting Game
            if (patrolWaiting)
            {
                waiting = true;
                waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }
        }

        //If we are waiting
        if(waiting)
        {
            waitTimer += Time.deltaTime;
            if(waitTimer >= totalWaitTime)
            {
                waiting = false;
                ChangePatrolPoint();
                SetDestination();
            }
        }
    }

    //Setting The Destination
    private void SetDestination()
    {
        if(patrolPoints != null)
        {
            Vector3 targetVector = patrolPoints[currentPatrolPoint].transform.position;
            navMeshAgent.SetDestination(targetVector);
            travelling = true;
            //Set Anim travelling to true
        }
    }

    //Select Patrol Point from list
    //small chance to send NPC backwards in node path 
    private void ChangePatrolPoint()
    {
        if(UnityEngine.Random.Range(0f, 1f) <= probabilitySwitch)
        {
            patrolForward = !patrolForward;
        }
        
        if(patrolForward)
        {
            /* USE MODULUS INSTEAD
            currentPatrolPoint++;
            if(currentPatrolPoint >= patrolPoints.Count)
            {
                currentPatrolPoint = 0;
            }
            */

            //MODULUS
            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Count;
        }
        else
        {
            /*Use Pre Process
            currentPatrolPoint--;
            if(currentPatrolPoint < 0)
            {
                currentPatrolPoint = patrolPoints.Count - 1;
            }
            */

            //Pre Process Quick Decrement then check and -
            if(--currentPatrolPoint < 0)
            {
                currentPatrolPoint = patrolPoints.Count - 1;
            }
        }
    }

    
}
