using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealth : Killable
{
    public float _health;
    private void Start()
    {      
        MaxHealth = 100;
        Health = 100;
        
    }

    // Update is called once per frame
    void Update()
    {
        _health = Health;
        //if (flashTimeRemaining > 0)
        //{
        //    flashTimeRemaining -= Time.deltaTime;// Subtract Timer to break out of Co routine
        //}

        //  if (Input.GetKeyDown(KeyCode.D))
        //  {
        //      HurtEnemy(1);
        //  }
    }   
}
