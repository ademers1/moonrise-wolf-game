using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseMovement : MonoBehaviour
{
    public float speed = 10f;
    public float lookRadius = 10f;
    int current = 0;
    GameObject target;
    public Transform[] runLocations;
    float WPradius = 1f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
       
    }

    // Update is called once per frame
    void Update()
    {
        
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if(distance <= lookRadius)
        {
           if(Vector3.Distance(runLocations[current].position, transform.position) < WPradius)
            {
                current++;
                if(current >= runLocations.Length)
                {
                    current = 0;
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, runLocations[current].position, speed * Time.deltaTime);
        }
 
    
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Gate"))
        {
            Destroy(gameObject);
        }
    }
}
