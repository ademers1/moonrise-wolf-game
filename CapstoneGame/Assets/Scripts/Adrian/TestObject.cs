using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : PoolObject
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {

        Destroy();
        }
    }

    
}
