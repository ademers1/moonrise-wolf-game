using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    GameObject enemy;
    public float nextDash = 1;
    public float dashCooldown = 2;
    public float dashSpeed = 5f;
    private bool isDashing = false;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Enemy");
        
    }

    // Update is called once per frame
    void Update()
    {
       

        if(Input.GetButtonDown("Fire1") && Time.time > nextDash)
        {
            nextDash = Time.time + dashCooldown;
            isDashing = true;
        }
        if(isDashing)
        {
            Attack();
        }
        else
        {
            isDashing = false; 
        }
    }

    public void Attack()
    {
        Vector3 move = transform.right + transform.forward;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(2);
        }
    }
}
