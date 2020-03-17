using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movement code - DON'T MOVE CHARACTERS LIKE THIS - THIS IS JUST AN EXAMPLE
        float moveValue = Input.GetAxis("Horizontal");
        if (moveValue != 0)
        {
            transform.Translate(new Vector3(moveValue, transform.position.y, transform.position.z));
        }

        //Call the Save System's Save Player function when you press 1. Pass it the current Player script component.
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveSystem.SavePlayer(this);
        }

        //Call the Save System's Load Player function
        if (Input.GetKeyDown(KeyCode.F6))
        {
            //Load player returns type PlayerData
            PlayerData data = SaveSystem.LoadPlayer();

            if (data != null)
            {
                transform.position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
            }
        }
    }
}
