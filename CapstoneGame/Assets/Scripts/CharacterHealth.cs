using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterHealth : MonoBehaviour
{
    private float health;
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
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount -= health/startHealth;
    }
    public void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Game");
    }
}
