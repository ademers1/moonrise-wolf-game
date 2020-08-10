using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum AnimState
{
    isIdle, isMoving, isAttacking, isJumping, isSneaking, isDashing, isHowling, isTailwhiping
}
public class PlayerController : MonoBehaviour
{

    public Transform cam;
    CharacterController controller;

    public float speed = 6f;

    public float rotationSpeed = 10f;

    public float turnSmoothTime = 0.05f;
    float turnSmoothVelocity;

    Rigidbody rb;
    Animator anim;

    //movement values
    private float currentSpeed;
    public float walkSpeed = 6f;
    public float runSpeed = 10f;
    public float stalkSpeed = 2.5f;
    public float jumpForce = 100.0f;
    public float dashForce = 1000f;
    private float t = 0;//this variable will be discontinued with animation events

    private int dashesRemaining = 3;
    public float dashChargeTime = 3.0f;
    public float dashTimer = 0f;

    public float howlCooldownAmount = 5.0f;
    public float howlTimer = 0;
    public float howlRadius = 10f;
    private bool howlOnCooldown;
    private LayerMask enemies;



    //**ATTACK**//

    //fury
    public Image furyBar;
    private float furyTimer;
    private float furyDamageModifier = 2.0f;
    private float fillAmount = 0.4f;

    //tail whip variables
    public float tailWhipCooldownAmount = 10.0f;
    public float tailWhipTimer = 0.0f;
    public float tailWhipRadius = 10.0f;
    private bool tailWhipOnCooldown;
    public float knockBackStrength = 10f;
    public float knockUpStrength = 5f;

    //attack variables
    public Transform attackPoint;
    public float attackDamage = 10f;
    public float heavyAttackDamage = 25f;
    public float attackRange = 0.5f;
    public float attackCooldown = 2.0f;
    public float heavyAttackCooldown = 10.0f;
    private float attackTimer = 0f;
    private float heavyAttackTimer = 0f;
    private bool heavyAttackOnCooldown;
    private bool attackOnCooldown;

    Vector3 moveDir = new Vector3(0, 0, 1);

    private AnimState _animState = AnimState.isIdle;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        enemies = LayerMask.GetMask("Enemies");
        furyBar.fillAmount = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame

    private void Update()
    {
        //recharge dashes if we don't currently have 3
        if (dashesRemaining < 3)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer > dashChargeTime)
            {
                dashTimer = 0;
                dashesRemaining++;
            }
        }

        //stop dashing after a second to set state back to idle 
        //will happen in animation event later
        if (_animState == AnimState.isDashing)
        {
            t += Time.deltaTime;
            if (t > 1)
            {
                _animState = AnimState.isIdle;
            }
        }

        //if jumping check if we hit ground to go back to idle
        if (_animState == AnimState.isJumping)
        {
            if (isGrounded())
            {
                _animState = AnimState.isIdle;
            }
        }

        //attacks
        if (_animState == AnimState.isIdle || _animState == AnimState.isMoving || _animState == AnimState.isSneaking)
        {
            if (Input.GetButtonDown("Fire1") && !attackOnCooldown)
            {
                _animState = AnimState.isAttacking;
                anim.SetTrigger("isAttacking");
                attackOnCooldown = true;
                Attack();
            }

            if (Input.GetButtonDown("Fire2") && !heavyAttackOnCooldown)
            {
                _animState = AnimState.isAttacking;
                anim.SetTrigger("isAttacking");
                heavyAttackOnCooldown = true;
                HeavyAttack();
            }
        }

        //set cooldown of attack and heavy attack
        if (attackOnCooldown)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= 0.5f)
            {
                _animState = AnimState.isIdle;
            }
            if (attackTimer >= attackCooldown)
            {
                attackTimer = 0;
                attackOnCooldown = false;
            }
        }

        if (heavyAttackOnCooldown)
        {
            heavyAttackTimer += Time.deltaTime;
            if (heavyAttackTimer >= 0.5f)
            {
                _animState = AnimState.isIdle;
            }
            if (heavyAttackTimer >= heavyAttackCooldown)
            {
                heavyAttackTimer = 0;
                heavyAttackOnCooldown = false;
            }
        }

        //howl
        if (_animState == AnimState.isIdle || _animState == AnimState.isMoving || _animState == AnimState.isSneaking)
        {
            if (Input.GetKeyDown(KeyCode.Q) && !howlOnCooldown)
            {
                //grab all the enemies in howl radius and loop through setting them to stunned
                _animState = AnimState.isIdle;
                Collider[] colliders = Physics.OverlapSphere(transform.position, tailWhipRadius, enemies);
                howlOnCooldown = true;

                foreach (Collider enemy in colliders)
                {
                    enemy.GetComponent<EnemyStatus>().Effect(0);
                }
                //TODO: start howling cooldown at end of animation

            }
        }
        //handles howl cooldown recharge
        if (howlOnCooldown)
        {
            howlTimer += Time.deltaTime;
            if (howlTimer >= howlCooldownAmount)
            {
                howlOnCooldown = false;
                howlTimer = 0f;
            }
        }

        //tailwhip
        if (_animState == AnimState.isIdle || _animState == AnimState.isMoving || _animState == AnimState.isSneaking)
        {
            if (Input.GetKeyDown(KeyCode.R) && !tailWhipOnCooldown)
            {
                _animState = AnimState.isIdle;
                Collider[] colliders = Physics.OverlapSphere(transform.position, howlRadius, enemies);
                tailWhipOnCooldown = true;

                foreach (Collider enemy in colliders)
                {
                    enemy.GetComponent<Rigidbody>().isKinematic = false;

                }
                Knockback(colliders);
            }
        }

        if (tailWhipOnCooldown)
        {
            tailWhipTimer += Time.deltaTime;
            if (tailWhipTimer > 2f)
            {
                GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemys)
                {
                    if (!enemy.GetComponent<Rigidbody>().isKinematic)
                    {
                        enemy.GetComponent<Rigidbody>().isKinematic = true;
                    }
                }
            }
            if (tailWhipTimer >= tailWhipCooldownAmount)
            {
                tailWhipTimer = 0;
                tailWhipOnCooldown = false;
            }
        }
    }
    void FixedUpdate()
    {

        if (_animState == AnimState.isIdle || _animState == AnimState.isMoving || _animState == AnimState.isSneaking)
        {

            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            t = 0;
            if (direction.magnitude >= 0.1f)
            {
                _animState = AnimState.isMoving;
                //get the angle we move and change the camera to match that angle

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                //here we get movedir which is the direction the camera is facing so we're always moving forward
                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, targetAngle, 0f), Time.time * 1f);
                rb.velocity = moveDir.normalized * speed;
             
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _animState = AnimState.isSneaking;
                rb.velocity = moveDir.normalized * speed * 0.5f;
            }
         

            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", true);
        }

        if (_animState == AnimState.isIdle || _animState == AnimState.isMoving || _animState == AnimState.isSneaking && dashesRemaining > 0)
        {
            //left ctrl or left mouse button
            if (Input.GetKeyDown(KeyCode.Z))
            {
                dashesRemaining--;
                _animState = AnimState.isDashing;
                Vector3 dashDirection;
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                if (vertical != 0 || horizontal == 0)
                {
                    //if a key isn't held down 
                    //or forward/back is held down
                    //then we want to dash straight ahead
                    horizontal = 0;
                    dashDirection = transform.forward;
                    Debug.Log(dashDirection);
                    rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);
                }
                else
                {
                    //else we want to dash to the side
                    //currently not working as we start moving to the side first
                    //TODO: figure out a new key to press for sideways dashes
                    if (horizontal < 0)
                    {
                        dashDirection = new Vector3(-1, 0, 0);
                        rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);
                    }
                    else
                    {
                        dashDirection = new Vector3(1, 0, 0);
                        rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);
                    }
                }
            }
        }

        //jump logic
        if (_animState == AnimState.isMoving && isGrounded())
        {
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log(isGrounded());
                _animState = AnimState.isJumping;
                //rb.AddForce(transform.up * 10, ForceMode.Impulse);
                rb.velocity = rb.velocity + Vector3.up * jumpForce;
            }
        }

    }

    //basic ground check by raycast
    private bool isGrounded()
    {
        RaycastHit ray;
        LayerMask ground = LayerMask.GetMask("Ground");
        if (Physics.Raycast(transform.position + new Vector3(0, 0.3f, 0), Vector3.down, out ray, .4f, ground))
        {
            return true;
        }
        return false;
    }

    //basic attack
    public void Attack()
    {

        // Dectect Range of Enemy
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemies);
        // Damage the Enemy 
        //TODO: make this only target one enemy
        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("tets");
            enemy.GetComponent<NPCHealth>().HurtEnemy(attackDamage);
        }
    }

    public void HeavyAttack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemies);
        // Damage the Enemy
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<NPCHealth>().HurtEnemy(heavyAttackDamage);
        }
    }

    private void Knockback(Collider[] colliders)
    {
        foreach (Collider enemy in colliders)
        {
            enemy.GetComponent<Rigidbody>().AddExplosionForce(knockBackStrength, transform.position, tailWhipRadius, knockUpStrength, ForceMode.Impulse);
        }
    }
}
