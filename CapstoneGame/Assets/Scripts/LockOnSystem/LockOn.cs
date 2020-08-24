using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LockOn : MonoBehaviour
{
    [SerializeField] private float lockOnRadius;
    [SerializeField] private float lockOnStopRadius;
    public LayerMask enemyLayers;
    public bool wolfLockOn;
    GameObject closestEnemy;
    GameObject previousClosestEnemy;
    CinemachineTargetGroup CMGroup;
    GameObject CMObj;
    bool targetLocked;
    GameObject playerObj;
    [SerializeField] Camera cam;

    public GameObject ClosestEnemy { get { return closestEnemy; } }

    // Start is called before the first frame update
    void Start()
    {
        lockOnRadius = 15.0f;
        lockOnStopRadius = 10.0f;
        CMObj = GameObject.Find("CM TargetGroup1");
        CMGroup = CMObj.GetComponent<CinemachineTargetGroup>();
        targetLocked = false;
        playerObj = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            wolfLockOn = !wolfLockOn;
        }


        if (wolfLockOn)
        {
            WolfLockOn();
            //turn this on and disable RotateToMatch extension from Cinemachine to work
            //playerObj.transform.LookAt(closestEnemy.transform);
        }
        else
        {
            if (previousClosestEnemy != null)
            {
                //lock off
                CMGroup.RemoveMember(previousClosestEnemy.transform);
                closestEnemy = null;
                previousClosestEnemy = null;
            }
        }
    }


    void WolfLockOn()
    {
        float shortestDistance = Mathf.Infinity;
        closestEnemy = null;

        Collider[] colliders = Physics.OverlapSphere(transform.position, lockOnRadius, enemyLayers);
        if (colliders.Length == 0)
        {
            wolfLockOn = false;
            return;
        }

        foreach (Collider enemy in colliders)
        {
            
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestEnemy = enemy.gameObject;
            }
        }
        // first closest enemy
        if (!previousClosestEnemy)
        {
            CMGroup.AddMember(closestEnemy.transform, 1, 1);
            Debug.Log("change 1");
            previousClosestEnemy = closestEnemy;
        }
        //Press a button to switch to different closest enemy
        else if (previousClosestEnemy != closestEnemy)
        {
            Debug.Log("change 2");
            if (Input.GetKey(KeyCode.B))
            {
                Debug.Log("change 3");
                CMGroup.RemoveMember(previousClosestEnemy.transform);
                CMGroup.AddMember(closestEnemy.transform, 1, 1);
                Debug.Log("change 4");
                previousClosestEnemy = closestEnemy;
            }
        }
        
        



    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, lockOnRadius);
    }
}
