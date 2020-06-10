using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSense : MonoBehaviour
{
    [SerializeField] private float senseRadius;
    [SerializeField] private float senseStopRadius;
    [SerializeField] private float senseTimer;
    bool wolfSenseOn;
    public LayerMask enemyLayers;
    float shortesDistance;
    GameObject closestEnemy;

    public GameObject ClosestEnemy { get { return closestEnemy; } }
    public bool WolfSenseOn { get { return wolfSenseOn; } }

    // Start is called before the first frame update
    void Start()
    {
        senseRadius = 30.0f;
        senseStopRadius = 200.0f;
        senseTimer = 1.0f;
        wolfSenseOn = false;
        shortesDistance = Mathf.Infinity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            wolfSenseOn = !wolfSenseOn;
        }
        if (wolfSenseOn)
        {
            WolfSensing();
        }
        else
        {
            closestEnemy.GetComponent<WolfSenseMaterial>().beingSensed = false;
        }
    }

    void WolfSensing()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, senseRadius, enemyLayers);
        foreach (Collider enemy in colliders)
        {
            enemy.GetComponent<WolfSenseMaterial>().beingSensed = false;

            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < shortesDistance)
            {
                shortesDistance = distance;
                closestEnemy = enemy.gameObject;
            }
        }
        closestEnemy.GetComponent<WolfSenseMaterial>().beingSensed = true;
        wolfSenseOn = true;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, senseRadius);
    }
}
