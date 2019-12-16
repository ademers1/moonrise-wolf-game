
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
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
        if (health <= 0)
        {
            Die();
        }
     
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;

    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
