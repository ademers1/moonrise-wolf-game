using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Start is called before the first frame update

    public float ButtonCooler = 0.5f ; // Half a second before reset
    public int ButtonCount = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //abilities
        if (Input.GetKeyDown("1"))
        {
           ability1();
        }

        if (Input.GetKeyDown("2"))
        {
            ability2();
        }

        if (Input.GetKeyDown("3"))
        {
            ability3();
        }

        //combat roll
        if (Input.GetKeyDown("d"))
        {

            if (ButtonCooler > 0 && ButtonCount == 1/*Number of Taps you want Minus One*/)
            {
                //Has double tapped
                transform.position += transform.right * (GetComponent<PlayerMovement>().currentSpeed * 2);
            }
            else
            {
                ButtonCooler = 0.5f;
                ButtonCount += 1;
            }
        }
        else if (Input.GetKeyDown("a"))
        {

            if (ButtonCooler > 0 && ButtonCount == 1/*Number of Taps you want Minus One*/)
            {
                //Has double tapped
                transform.position -= transform.right * (GetComponent<PlayerMovement>().currentSpeed * 2);
            }
            else
            {
                ButtonCooler = 0.5f;
                ButtonCount += 1;
            }
        }
        if (Input.GetKeyDown("w"))
        {

            if (ButtonCooler > 0 && ButtonCount == 1/*Number of Taps you want Minus One*/)
            {
                //Has double tapped
                transform.position += transform.forward * (GetComponent<PlayerMovement>().currentSpeed * 2);
            }
            else
            {
                ButtonCooler = 0.5f;
                ButtonCount += 1;
            }
        }
        else if (Input.GetKeyDown("s"))
        {

            if (ButtonCooler > 0 && ButtonCount == 1/*Number of Taps you want Minus One*/)
            {
                //Has double tapped
                transform.position -= transform.forward * (GetComponent<PlayerMovement>().currentSpeed * 2) ;
            }
            else
            {
                ButtonCooler = 0.5f;
                ButtonCount += 1;
            }
        }

        if (ButtonCooler > 0)
        {

            ButtonCooler -= 1 * Time.deltaTime;

        }
        else
        {
            ButtonCount = 0;
        }
    }


    private void ability1()
    {
        Debug.Log("ability 1 was pressed");
    }

    private void ability2()
    {
        Debug.Log("ability 2 was pressed");
    }

    private void ability3()
    {
        Debug.Log("ability 3 was pressed");
    }
}

