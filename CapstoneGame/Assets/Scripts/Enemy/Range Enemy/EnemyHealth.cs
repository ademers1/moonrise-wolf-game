
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float startHealth;
    public Image healthBar;

    string enemyHit = "EnemyHit";

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
        //updates health bar every frame
        healthBar.fillAmount = health / startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;
        GameManager.Instance.PlaySound(enemyHit);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
