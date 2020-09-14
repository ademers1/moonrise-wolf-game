using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public PoolTest poolTest;

    private void Start()
    {
        GameEvents.current.OnSpawnTriggerEnter += OnSpawnTriggerEntered; 
    }


    private void OnSpawnTriggerEntered()
    {
        poolTest.SpawnEnemyFromPool();
    }

    private void OnDestroy()
    {
        GameEvents.current.OnSpawnTriggerEnter -= OnSpawnTriggerEntered;
    }
}
