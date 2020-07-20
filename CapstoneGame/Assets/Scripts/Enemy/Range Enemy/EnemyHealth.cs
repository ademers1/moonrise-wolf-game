
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Killable
{
    public Image healthBar;
    string enemyHit = "EnemyHit";

    void Start()
    {
        Health = 100;       
    }

    void Update()
    {
        //updates health bar every frame
        healthBar.fillAmount = Health / MaxHealth;
    }

    public void TakeDamage(float amount)
    {
        Health -= (int)amount;
        healthBar.fillAmount = Health / MaxHealth;
        GameManager.Instance.PlaySound(enemyHit);
    }
}
