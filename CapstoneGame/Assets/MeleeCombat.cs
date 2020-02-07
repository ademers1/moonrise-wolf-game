using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    GameObject player;
    public Transform attackPoint;
    public float attackDamage = 5f;
    public float attackRange = 0.5f;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public LayerMask playerLayer;
    public bool canAttack;
    void Start()
    {
        player = GameObject.Find("Player");
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canAttack)
        {
            if(Time.time >= nextAttackTime)
            {
                EnemyAttack();
                nextAttackTime = Time.time + 1 / attackRate;
            }
        }
    }


    public void EnemyAttack()
    {
        Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);
        foreach (Collider player in hitPlayer)
        {
            player.GetComponent<CharacterHealth>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
