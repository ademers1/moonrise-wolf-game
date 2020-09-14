using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolTest : MonoBehaviour
{
    public GameObject prefab;
    public Transform[] spawnLocations;
    int rand;
    int locationIndex = 3;
    private void Start()
    {
        PoolManager.instance.CreatePool(prefab, 3);
        /*Instantiate(spawnLocations[0], transform.position, Quaternion.identity);
        Instantiate(spawnLocations[1], transform.position, Quaternion.identity);
        Instantiate(spawnLocations[2], transform.position, Quaternion.identity);
        Instantiate(spawnLocations[3], transform.position, Quaternion.identity);
       
        System.Random rnd = new System.Random();
        spawnLocations = spawnLocations.OrderBy(x => rnd.Next()).ToArray();*/


        //Transform[] newSpawnLocations = spawnLocations.OrderBy(x => rnd.Next()).ToArray();
        //for (int i = 0; i < newSpawnLocations.Length; i++)
        //{
        //    print(spawnLocations[i].position + " | " + newSpawnLocations[i].position);
        //}
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PoolManager.instance.AddToPool(prefab);
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            RandomizeSpawnPositions();            
            for (int i = 0; i < spawnLocations.Length; i++)
            {
                PoolManager.instance.ReuseObject(prefab, spawnLocations[i].position, Quaternion.identity);
               
            }
        }
       
    }
    public void SpawnEnemyFromPool()
    {
        //Transform spawnLocation = spawnLocations[Array.IndexOf(triggerBoxes, triggerBox)];
        RandomizeSpawnPositions();
        for (int i = 0; i < spawnLocations.Length; i++)
        {
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(spawnLocations[i].position);
            bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            if (!onScreen)
            {
                PoolManager.instance.ReuseObject(prefab, spawnLocations[i].position, Quaternion.identity);
                Debug.Log(spawnLocations[i].position);
            }

        }
    }
  
    void RandomizeSpawnPositions()
    {
        System.Random rnd = new System.Random();
        spawnLocations = spawnLocations.OrderBy(x => rnd.Next()).ToArray();
    }
}
