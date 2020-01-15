using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    GameObject enemy;
    public Transform attackPoint;
    public float attackDamage = 2f;
    public float attackRange = 0.5f;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public LayerMask enemyLayers;
    public bool canAttack;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Enemy");
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack)
        {
            if (Time.time >= nextAttackTime)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                    Debug.Log("Player Attacked!");
                }
            }
        }
    }

    public void Attack()
    {
        // Play animation for attack
        // Dectect Range of Enemy
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        // Damage the Enemy
        foreach(Collider enemy in hitEnemies)
        {
             enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            Debug.Log("Enemy Hit!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
