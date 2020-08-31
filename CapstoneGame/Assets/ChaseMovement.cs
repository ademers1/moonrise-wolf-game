using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ChaseMovement : MonoBehaviour
{
    public float speed = 10f;
    public float lookRadius = 10f;
    public NavMeshAgent agent;
    GameObject target;
    Rigidbody rb;
    public float runTimer = 1.2f;
    public Transform[] runLocations;
    private int pointIndex;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.speed = 10f;
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                RunAway();
            }
        }
        if(distance >= lookRadius)
        {
            agent.speed = 5f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void RunAway()
    {
       if(runLocations.Length == 0)
        {
            return;
        }
        agent.destination = runLocations[pointIndex].position;

        pointIndex = (pointIndex + 1) % runLocations.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gate"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator IndexChange()
    {
        yield return new WaitForSeconds(runTimer);
     
    }
}
