using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Assets.Code
{
    public class SmartWaypoint : Waypoint
    {
        

        [SerializeField]
        protected float connectedRadius = 100f;

        List<SmartWaypoint> connections;


        // Start is called before the first frame update
        void Start()
        {
            //Locate all waypoints in scene
            GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

            //Create a list of waypoints to reference
            connections = new List<SmartWaypoint>();

            //Check To see if Waypoint is connected
            for(int i = 0; i < allWaypoints.Length; i++)
            {
                SmartWaypoint nextWaypoint = allWaypoints[i].GetComponent<SmartWaypoint>();

                //If Waypoint Found
                if(nextWaypoint != null)
                {
                    if(Vector3.Distance(this.transform.position, nextWaypoint.transform.position) <= connectedRadius && nextWaypoint != this)
                    {
                        connections.Add(nextWaypoint);
                    }
                }
            }
        }

        //Draw the Gizmo to see in engine
        public override void OnDrawGizmo()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, debugDrawRadius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, connectedRadius);
        }

        public SmartWaypoint NextWaypoint(SmartWaypoint previousWaypoint)
        {
            if(connections.Count == 0)
            {
                //No Waypoints available
                Debug.LogError("Not Enough Waypoints, you absolute darling of a man you ;) ");
                return null;
            }
            else if(connections.Count == 1 && connections.Contains(previousWaypoint))
            {
                //Only Waypoint in range is the previous one so backtrack / Dead End
                return previousWaypoint;
            }
            else
            {
                SmartWaypoint nextWaypoint;
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
