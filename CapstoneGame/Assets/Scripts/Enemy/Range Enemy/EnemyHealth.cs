
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float startHealth;
    public Image healthBar;
    public GameObject Wall;
    


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
        Wall.GetComponent<WallBlockage>().EnemiesToDefeat.Remove(gameObject);
        Destroy(gameObject);
    }
}
