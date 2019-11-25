using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//Version 1;

public class betaWolf : MonoBehaviour
{
    public Transform target; // Will generally be the player.

    private NavMeshAgent nav;

    PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(target.position);

        
        //if player is standing still
        if (player.currentSpeed == 0)
        {
            //test if betaWolves are close enough to alpha to stop
            if (nav.remainingDistance <= 3.0f)
                nav.speed = 0.0f;
            else // else the beta wolves must move towards the alpha but at a slower speed cause why not
                nav.speed = player.walkSpeed;
        }
        else
        {
            if (nav.remainingDistance <= 1.5f)
                nav.speed = 0.0f;
            else // else the beta wolves must move towards the alpha but at a slower speed cause why not
                nav.speed = player.currentSpeed / 1.2f;
        }
        
    }
}
