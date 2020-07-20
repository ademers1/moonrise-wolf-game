using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Killable : MonoBehaviour, IKillable
{
    public virtual int Health
    {
        get
        {
            return _health;
        }
        set
        {
            if (Invulnerable) return;
            if (value > MaxHealth)
                _health = MaxHealth;
            else _health = value;
            if (_health <= 0)
                Die();
        }
    }
    public bool Invulnerable { get; set; } = false;
    private int _health;
    public int MaxHealth { get; set; } = 100;

    public void Die()
    {
        if (gameObject.tag == "Enemy")
        {
            GameManager.Instance.EnemyKilled();
        }
        if (gameObject.tag == "Player")
        {
            GameManager.Instance.Died();
        }
        Destroy(gameObject);
    }


}
