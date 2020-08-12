using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealth : MonoBehaviour
{

    public float enemyMaxhealth;
    public float enemyCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        SetMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyCurrentHealth <= 0)
        {
            //Replace with enemy death animation
            Destroy(gameObject, 2f);
        }

     //  if (Input.GetKeyDown(KeyCode.D))
     //  {
     //      HurtEnemy(1);
     //  }
    }

    public void HurtEnemy(float damageToGive)
    {
        enemyCurrentHealth -= damageToGive;
    }

    public void SetMaxHealth()
    {
        enemyCurrentHealth = enemyMaxhealth;
    }

    
}
