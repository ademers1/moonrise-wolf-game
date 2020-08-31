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
    public float runTimer = 2f;
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
            RunAway();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void RunAway()
    {
            if (runLocations.Length == 0) return;
            if (Vector3.Distance(this.transform.position, runLocations[pointIndex].position) < 0.3f)
            {
                pointIndex++;
                if (pointIndex >= runLocations.Length)
                {
                    pointIndex = 0;
                }
            }
        agent.SetDestination(runLocations[pointIndex].position);
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
        pointIndex++;
        agent.destination = runLocations[pointIndex].position;
    }
}
