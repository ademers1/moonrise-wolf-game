using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;


namespace Assets.Code
{
    public class NPC_SmartPatrol : MonoBehaviour
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

        //Time to sit at Node
        [SerializeField]
        float sitThere = 2f;

        //Base Behaviors
        NavMeshAgent navMeshAgent;
        SmartWaypoint currentWaypoint;
        SmartWaypoint previousWaypoint;

        bool travelling;
        bool waiting;
        float waitTimer;
        int wayPointsVisited;


        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent = this.GetComponent<NavMeshAgent>();

            if(navMeshAgent == null)
            {
                Debug.LogError("There is no SMARTPATROL NavMeshAgent on the object " + gameObject.name + " you DOLT!!!");
            }
            else
            {
                if(currentWaypoint == null)
                {
                    //Set Random Waypoint
                    //Get all Waypoints
                    GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

                    if(allWaypoints.Length > 0)
                    {
                        while (currentWaypoint == null)
                        {
                            int random = UnityEngine.Random.Range(0, allWaypoints.Length);
                            SmartWaypoint startingWaypoint = allWaypoints[random].GetComponent<SmartWaypoint>();

                            //Waypoint Found
                            if(startingWaypoint != null)
                            {
                                currentWaypoint = startingWaypoint;
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("We combed the beach sir, and we ain't found shit!! (no waypoints)");
                    }
                }

                SetDestination();
            }
        }

        // Update is called once per frame
        void Update()
        {
            //Check to see if close to destination
            if(travelling && navMeshAgent.remainingDistance <= 1.0f)
            {
                travelling = false;
                wayPointsVisited++;

                //If we are waiting, wait
                if(patrolWaiting)
                {
                    waiting = true;
                    waitTimer = 0f;
                }
                else
                {
                    SetDestination();
                }
            }

            //If we are currently waiting
            if(waiting)
            {
                waitTimer += sitThere * Time.deltaTime;
                if(waitTimer >= totalWaitTime)
                {
                    waiting = false;

                    SetDestination();
                }
            }
        }

        private void SetDestination()
        {
            if(wayPointsVisited > 0)
            {
                SmartWaypoint nextWaypoint = currentWaypoint.NextWaypoint(previousWaypoint);
                previousWaypoint = currentWaypoint;
                currentWaypoint = nextWaypoint;
            }

            Vector3 targetVector = currentWaypoint.transform.position;
            navMeshAgent.SetDestination(targetVector);
            travelling = true;
            //Set Animator to Travelling True Bool
        }
    }
}