using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class controller : MonoBehaviour
{
    public Transform followObject;
    private NavMeshAgent nav;

    // Start is called before the first frame update
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(followObject.position);

        if (Input.GetKey(KeyCode.Space))
        {
            setSpeed();
        }
        else
        {
            nav.speed = 3.5f;
        }
    }

    void setSpeed()
    {
        nav.speed += 5.1f;
    }
}
