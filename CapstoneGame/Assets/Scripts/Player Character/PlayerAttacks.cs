using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerAttacks : MonoBehaviour
{
    //attack variables
    public Image furyBar;
    public float furyTimer;
    public float furyAttackDamage = 20f;
    public float basicAttackFill = 0.15f;
    public float heavyAttackFill = 0.25f;
    public Transform attackPoint;
    public float nextTailWhipTime;
    public float attackDamage = 10f;
    public float heavyAttackDamge = 25f;
    public float attackRange = 0.5f;
    public float attackRate = 2f;
    public float heavyAttackRate = 10f;
    float nextAttackTime = 0f;
    float nextHeavyAttackTimer;
    float nextHeavyAttack;
    public LayerMask enemyLayers;
    public bool canAttack;
    [SerializeField] private float knockbackStrength;
    [SerializeField] private float knockUpStrength;
    [SerializeField] private float knockbackRadius;
    private float radius;

    Animator anim;

    private PlayerJumpState state;
    private PlayerAnimationState animState;

    AudioSource wolfAudio;
    public AudioClip growl;
    enum PlayerJumpState
    {
        isGrounded, isAirborn
    }
    enum PlayerAnimationState
    {
        isIdle, isMoving, isAttacking
    }
    void Start()
    {
        furyBar.fillAmount = 0;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animState == PlayerAnimationState.isIdle || animState == PlayerAnimationState.isMoving)
        {
            if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
              //  wolfAudio.PlayOneShot(growl);
                //animState = PlayerAnimationState.isAttacking;
                anim.SetTrigger("isAttacking");
            }
        }

        if (Time.time > nextHeavyAttack)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                nextHeavyAttackTimer += Time.deltaTime;
                //animState = PlayerAnimationState.isAttacking;
                anim.SetTrigger("isAttacking");
            }
            if (Input.GetKeyUp(KeyCode.Mouse1) && nextHeavyAttackTimer > 3)
            {
                HeavyAttack();
                wolfAudio.PlayOneShot(growl);
                //animState = PlayerAnimationState.isAttacking;
                anim.SetTrigger("isAttacking");
            }
        }


        if (Time.time >= nextTailWhipTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                KnockBack();
            }
        }
    }
       
            if (furyBar.fillAmount >= 1)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    furyTimer += Time.deltaTime;
                    FuryMode();
                   
                 if(Time.time >= furyTimer)
                 {
                     DefaultMode();
}
                }
            }
    }

    //attacks
    #region
    public void Attack()
    {

        // Dectect Range of Enemy
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        // Damage the Enemy
        foreach (Collider enemy in hitEnemies)
        {

            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            furyBar.fillAmount += basicAttackFill;

        }
    }

    public void HeavyAttack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        // Damage the Enemy
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(heavyAttackDamge);
            furyBar.fillAmount += heavyAttackFill;
        }
    }
    public void KnockBack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, knockbackRadius);
        foreach (Collider enemy in colliders)
        {
            if (enemy.tag != "Player")
            {
                Rigidbody rb = enemy.GetComponent<Rigidbody>();
                Debug.Log(enemy.gameObject);
                if (rb != null)
                {

                    rb.AddExplosionForce(knockbackStrength, transform.position, knockbackRadius, knockUpStrength, ForceMode.Impulse);
                }
            }
        }
    }

    public void FuryMode()
    {
        // values Change to match fury mode;
        //faster attack speed;
        attackRate = 4f;
        //more damage;
        attackDamage = furyAttackDamage;
        //move movement speed;

    }

    public void DefaultMode()
    {
        //sets all the orginal values back;
        attackRate = 2f;
        attackDamage = 10f;
        furyBar.fillAmount = 0f;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(transform.position, knockbackRadius);
    }
    #endregion
}
