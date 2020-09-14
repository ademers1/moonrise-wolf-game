using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolTest : MonoBehaviour
{
    public GameObject prefab;
    public Transform[] spawnLocations;
    public GameObject[] triggerBoxes = new GameObject[4];
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
    public void SpawnEnemyFromPool(GameObject triggerBox, Collider player, GameObject boxCollider)
    {
        Transform spawnLocation = spawnLocations[Array.IndexOf(triggerBoxes, triggerBox)];
        if(Camera.main.WorldToViewportPoint(spawnLocation.position).x < 0 || Camera.main.WorldToViewportPoint(spawnLocation.position).x > 1 || Camera.main.WorldToViewportPoint(spawnLocation.position).y < 0 || Camera.main.WorldToViewportPoint(spawnLocation.position).y > 1)
        {
            boxCollider.GetComponent<BoxCollider>().enabled = false;
            Debug.Log(spawnLocation.position);
            //spawn object as spawn point should be invisible
            PoolManager.instance.ReuseObject(prefab, spawnLocation.position, Quaternion.identity);
        }
    }
  
    void RandomizeSpawnPositions()
    {
        System.Random rnd = new System.Random();
        spawnLocations = spawnLocations.OrderBy(x => rnd.Next()).ToArray();
    }
}
