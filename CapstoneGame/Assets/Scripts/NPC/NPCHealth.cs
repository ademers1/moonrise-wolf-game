using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealth : Killable
{

    public Vector3 enemyCurrentPosition;
    public float _health;
    // Start is called before the first frame update
    void Start()
    {    
        MaxHealth = 100;
        Health = 100;
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyCurrentPosition = this.gameObject.transform.position;   
    
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
