using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterHealth : MonoBehaviour
{
    public float health;
    public float startHealth;
    public Image healthBar;
   

    void Start()
    {
        
        health = startHealth;
    }

    void Update()
    {
     if(health <= 0)
        {
            Die();
        }
     //no over heal;
     else if(health > startHealth)
        {
            health = startHealth;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health/startHealth;
        Debug.Log("Current Health: " + health);
    }
    //added a healing function
    public void Heal(float amount)
    {
        health += amount;
        healthBar.fillAmount = health / startHealth;
        Debug.Log("Current Health: " + health);
    }
    public void Die()
    {
        Destroy(gameObject);
       
        SceneManager.LoadScene("Loss");
    }
}
