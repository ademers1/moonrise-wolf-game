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

    public GameObject ClosestEnemy { get { return closestEnemy; } }

    // Start is called before the first frame update
    void Start()
    {
        lockOnRadius = 25.0f;
        lockOnStopRadius = 15.0f;
        CMObj = GameObject.Find("CM TargetGroup1");
        CMGroup = CMObj.GetComponent<CinemachineTargetGroup>();
        targetLocked = false;
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
        float shortesDistance = Mathf.Infinity;
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
            if (distance < shortesDistance)
            {
                shortesDistance = distance;
                closestEnemy = enemy.gameObject;
            }
        }

        if (!previousClosestEnemy)
        {
            CMGroup.AddMember(closestEnemy.transform, 1, 1);
        }
        else if (previousClosestEnemy != closestEnemy)
        {
            CMGroup.RemoveMember(previousClosestEnemy.transform);
            CMGroup.AddMember(closestEnemy.transform, 1, 1);
        }

        previousClosestEnemy = closestEnemy;


    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, lockOnRadius);
    }
}
