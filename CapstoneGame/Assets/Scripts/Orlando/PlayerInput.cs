using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            Debug.Log("ability 1 was pressed");
            ability1();
        }

        if (Input.GetKeyDown("2"))
        {
            Debug.Log("ability 2 was pressed");
            ability2();
        }

        if (Input.GetKeyDown("3"))
        {
            Debug.Log("ability 3 was pressed");
            ability3();
        }
    }

    private void ability1()
    {

    }

    private void ability2()
    {

    }

    private void ability3()
    {

    }
}

