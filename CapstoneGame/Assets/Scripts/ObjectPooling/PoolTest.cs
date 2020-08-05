using System.Collections;
using System.Collections.Generic;
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
        Instantiate(spawnLocations[0], transform.position, Quaternion.identity);
        Instantiate(spawnLocations[1], transform.position, Quaternion.identity);
        Instantiate(spawnLocations[2], transform.position, Quaternion.identity);
        Instantiate(spawnLocations[3], transform.position, Quaternion.identity);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PoolManager.instance.AddToPool(prefab);
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            for (int i = 0; i < spawnLocations.Length; i++)
            {
                rand = Random.Range(0, spawnLocations.Length);
                PoolManager.instance.ReuseObject(prefab, spawnLocations[i].position, Quaternion.identity);
                locationIndex--;
                if (locationIndex <= -1)
                {
                    locationIndex = 3;
                }
            }
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag( "Player"))
        {

           // rand = Random.Range(0, spawnLocations.Length);
            PoolManager.instance.ReuseObject(prefab, spawnLocations[locationIndex].position, Quaternion.identity);
            locationIndex--;
            if (locationIndex <= -1)
            {
                locationIndex = 3;
            }

            // rand = Random.Range(0, spawnLocations.Length);
            // Instantiate(spawnLocations[rand], transform.position, Quaternion.identity);
            // PoolManager.instance.ReuseObject(prefab,spawnLocations[rand].position, Quaternion.identity);
        }
    }

}
