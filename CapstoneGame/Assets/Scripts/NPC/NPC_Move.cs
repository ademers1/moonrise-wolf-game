using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Move : MonoBehaviour
{

    [SerializeField]
    Transform destination;

    NavMeshAgent navMeshAgent;
    Rigidbody enemyRB;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        if(navMeshAgent == null)
        {
            Debug.Log("NavMeshAgent is not attached to: " + gameObject.name);
        }
        else
        {
            SetDestination();
        }

        enemyRB = GetComponent<Rigidbody>();

    }

    public void SetDestination()
    {
        
        if(destination != null)
        {
            Vector3 targetVector = destination.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
