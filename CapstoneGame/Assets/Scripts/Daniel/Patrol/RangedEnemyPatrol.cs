using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;


public class RangedEnemyPatrol : MonoBehaviour
{
    Rigidbody rb;
    public GameObject player;
    public GameObject enemy;
    public Transform playerTransform;
    public bool isRunning;
    public Animator anim;
    public MeshRenderer rend;
    NavMeshAgent agent;
    public GameObject key;
    public ParticleSystem particle;
    public Transform[] nodeObj;
    bool isPatrol;
    int randomSpot;
    bool alerted;
    public WolfLevel PlayerLevel;
    int xpValue = 50;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        rend = GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        randomSpot = Random.Range(0, nodeObj.Length);
        isPatrol = true;
        alerted = false;
        GoOnPatrol();
    }

    void Update()
    {

        if (Vector3.Distance(playerTransform.position, enemy.transform.position) < 15)
        {
            if (alerted == false) 
                alerted = true;
            if (isPatrol == true)
                isPatrol = false;
        }
        else if(Vector3.Distance(playerTransform.position, enemy.transform.position) >= 15)
        {
            if(alerted==true)
                alerted = false;
        }
        
        if (!alerted)
        {
            if(isPatrol==false)
                isPatrol = true;
            if (agent.isStopped == true)
                agent.isStopped = false;
            GoOnPatrol();
        }
        if(alerted)
        {
            if (isPatrol == true)
                isPatrol = false;
            if(agent.isStopped==false)
                agent.isStopped = true;
        }
    }
    
    void GoOnPatrol()
    {
        if (isPatrol == true)
        {
            agent.destination = nodeObj[randomSpot].position;
            if (Vector3.Distance(enemy.transform.position, nodeObj[randomSpot].position) < 1f)
            {
                randomSpot = Random.Range(0, nodeObj.Length);
            }
        }
    }
}
