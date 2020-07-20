using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Code.NPCCode
{

    public class NPCPatrolPoints : MonoBehaviour
    {
        [SerializeField]
        protected float debugDrawRadius = 1.0f;

        [SerializeField]
        protected float connectedRadius = 50f;

        List<NPCPatrolPoints> connections;


        // Start is called before the first frame update
        void Start()
        {
            //Locate all waypoints in scene
            GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

            //Create a list of waypoints to reference
            connections = new List<NPCPatrolPoints>();

            //Check To see if Waypoint is connected
            for (int i = 0; i < allWaypoints.Length; i++)
            {
                NPCPatrolPoints nextWaypoint = allWaypoints[i].GetComponent<NPCPatrolPoints>();

                //If Waypoint Found
                if (nextWaypoint != null)
                {
                    if (Vector3.Distance(this.transform.position, nextWaypoint.transform.position) <= connectedRadius && nextWaypoint != this)
                    {
                        connections.Add(nextWaypoint);
                    }
                }
            }
        }

        //Draw the Gizmo to see in engine //ASK BO WHY THEY ARE NOT APPEARING 
        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, debugDrawRadius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, connectedRadius);
        }

        public NPCPatrolPoints NextWaypoint(NPCPatrolPoints previousWaypoint)
        {
            if (connections.Count == 0)
            {
                //No Waypoints available
                Debug.LogError("Not Enough Waypoints, you absolute darling of a man you ;) ");
                return null;
            }
            else if (connections.Count == 1 && connections.Contains(previousWaypoint))
            {
                //Only Waypoint in range is the previous one so backtrack / Dead End
                return previousWaypoint;
            }
            else
            {
                NPCPatrolPoints nextWaypoint;
                int nextIndex = 0;

                do
                {
                    nextIndex = UnityEngine.Random.Range(0, connections.Count);
                    nextWaypoint = connections[nextIndex];
                } while (nextWaypoint == previousWaypoint);

                return nextWaypoint;
            }
        }



        // Update is called once per frame
        void Update()
        {

        }
    }
}
