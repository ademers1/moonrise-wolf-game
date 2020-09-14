using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public PoolTest poolTest;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Trigger");
        if (other.CompareTag("Player"))
        {
            
            poolTest.SpawnEnemyFromPool(gameObject, other, gameObject);
        }
    }
}
