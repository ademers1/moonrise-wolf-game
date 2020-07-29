using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolTest : MonoBehaviour
{
    public GameObject prefab;
    public Transform[] spawnLocations;
    int rand;
    private void Start()
    {
        PoolManager.instance.CreatePool(prefab, 3);
 
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PoolManager.instance.AddToPool(prefab);
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag( "Player"))
        {
            rand = Random.Range(0, spawnLocations.Length);
            Instantiate(spawnLocations[rand], transform.position, Quaternion.identity);
            PoolManager.instance.ReuseObject(prefab,spawnLocations[rand].position, Quaternion.identity);
        }
    }

}
