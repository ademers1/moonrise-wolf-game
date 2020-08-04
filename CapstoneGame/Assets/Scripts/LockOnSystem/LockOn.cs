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
    float shortesDistance;
    GameObject closestEnemy;
    CinemachineTargetGroup CMGroup;

    public GameObject ClosestEnemy { get { return closestEnemy; } }

    // Start is called before the first frame update
    void Start()
    {
        lockOnRadius = 30.0f;
        lockOnStopRadius = 200.0f;
        shortesDistance = Mathf.Infinity;
        CMGroup = this.GetComponent<CinemachineTargetGroup>();
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
    }

    void WolfLockOn()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, lockOnRadius, enemyLayers);
        foreach (Collider enemy in colliders)
        {

            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < shortesDistance)
            {
                shortesDistance = distance;
                closestEnemy = enemy.gameObject;
            }
            if (distance < lockOnRadius)
            {
                if(closestEnemy)
                {
                    //lock on
                    CMGroup.AddMember(closestEnemy.transform, 1, 1);
                }
                else
                {
                    //lock off
                    CMGroup.RemoveMember(closestEnemy.transform);
                    wolfLockOn = false;
                }
            }
            else
            {
                //lock off
                wolfLockOn = false;
                CMGroup.RemoveMember(closestEnemy.transform);
            }
        }
    }
}
