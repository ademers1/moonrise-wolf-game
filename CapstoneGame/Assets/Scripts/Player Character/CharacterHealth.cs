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

    public string whineSound = "WolfHurt";

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
     //updates health bar every frame
        healthBar.fillAmount = health / startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health/startHealth;
        Debug.Log("Current Health: " + health);
        GameManager.Instance.PlaySound(whineSound);
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
        GameManager.Instance.Camera.ShowMouse();
        SceneManager.LoadScene("Loss");
        
    }
}
