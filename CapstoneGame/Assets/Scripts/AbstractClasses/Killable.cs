using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public bool isAlive { 
        get 
        { 
            if (Health > 0) 
                return true;
            return false;
        }
        private set { }
    }

    private int _health;
    public int MaxHealth { get; set; } = 100;

    public float flashLength;
    protected float flashTimeRemaining;
    protected bool flashActive;

    [SerializeField]
    public SkinnedMeshRenderer meshRenderer;
    Color originalColor;

    public Slider slider;
    public Material primaryMat;
    public Material flashMat;


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

    IEnumerator FlashRoutine()
    {
        while (flashTimeRemaining > 0)
        {
            if (flashActive)
            {
                flashActive = false;
                meshRenderer.material = primaryMat;

            }
            else
            {
                flashActive = true;
                meshRenderer.material = flashMat;
            }
            yield return new WaitForSeconds(.1f);

        }
        flashActive = false;
        meshRenderer.material = primaryMat;

    }


}
