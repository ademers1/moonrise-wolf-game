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
            if (wolfSenseOn == false)
            {
                WolfSensing();
            }
            else if (wolfSenseOn == true)
            {
                WolfSensingStop();
            }
        }
        if(wolfSenseOn == true)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, senseRadius, enemyLayers);
            foreach (Collider enemy in colliders)
            {
                if (enemy.GetComponent<WolfSenseMaterial>() != null)
                {
                    enemy.GetComponent<WolfSenseMaterial>().beingSensed = true;
                }
                if (Vector3.Distance(enemy.transform.position, transform.position) > senseRadius && enemy.GetComponent<WolfSenseMaterial>() != null)
                {
                    enemy.GetComponent<WolfSenseMaterial>().beingSensed = false;
                }
                float distance = Vector3.Distance(enemy.transform.position, transform.position);
                if(distance < shortesDistance)
                {
                    shortesDistance = distance;
                    closestEnemy = enemy.gameObject;
                }

            }
        }
        

    }
    void WolfSensing()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, senseRadius, enemyLayers);
        foreach (Collider enemy in colliders)
        {
            if(enemy.GetComponent<WolfSenseMaterial>() != null)
            {
                enemy.GetComponent<WolfSenseMaterial>().beingSensed = true;
            }
            
        }
        wolfSenseOn = true;
    }
   void WolfSensingStop()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, senseStopRadius, enemyLayers);
        foreach (Collider enemy in colliders)
        {
            if (enemy.GetComponent<WolfSenseMaterial>() != null)
            {
                enemy.GetComponent<WolfSenseMaterial>().beingSensed = false;

            }
        }
        wolfSenseOn = false;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, senseRadius);
    }
}
