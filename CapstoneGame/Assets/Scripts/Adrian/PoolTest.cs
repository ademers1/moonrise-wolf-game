using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolTest : MonoBehaviour
{
    public GameObject prefab;

    private void Start()
    {
        PoolManager.instance.CreatePool(prefab, 3);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PoolManager.instance.ReuseObject(prefab, Vector3.zero, Quaternion.identity);
        }
    }
}
