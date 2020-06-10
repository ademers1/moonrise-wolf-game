using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathRenderer : MonoBehaviour
{
    public LineRenderer line; //to hold the line Renderer
    [SerializeField]
    Transform targetTransform; //to hold the transform of the target
    public NavMeshAgent agent; //to hold the agent of this gameObject
    GameObject closestEnemyGO;
    public WolfSense wolfSenseScript;
    void Awake()
    {
        line = GetComponent<LineRenderer>(); //get the line renderer
        agent = GameObject.Find("Player").GetComponent<NavMeshAgent>(); //get the agent
        wolfSenseScript = closestEnemyGO.GetComponent<WolfSense>();
    }

    private void Update()
    {
        closestEnemyGO = closestEnemyGO.GetComponent<WolfSense>().ClosestEnemy;
        getPath();
    }
    void getPath()
    {
        line.SetPosition(0, agent.transform.position); //set the line's origin

        agent.SetDestination(closestEnemyGO.transform.position); //create the path

        DrawPath(agent.path);

        agent.isStopped = true;//add this if you don't want to move the agent

    }

    void DrawPath(NavMeshPath path)
    {
        if (path.corners.Length < 2) //if the path has 1 or no corners, there is no need
            return;

        line.positionCount = path.corners.Length; //set the array of positions to the amount of corners

        for (var i = 1; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]); //go through each corner and set that to the line renderer's position
        }
    }
}
