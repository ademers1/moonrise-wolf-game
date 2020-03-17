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
    AudioSource wolfAudio1;

    public AudioClip whine;

    void Start()
    {
        
        health = startHealth;
        wolfAudio1 = GetComponent<AudioSource>();
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
        wolfAudio1.PlayOneShot(whine);
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
        Debug.Log("Entering Loss Scenario");
        Destroy(gameObject);
        GameManager.Instance.Camera.ShowMouse();
        SceneManager.LoadScene("Loss");
        
    }
}
