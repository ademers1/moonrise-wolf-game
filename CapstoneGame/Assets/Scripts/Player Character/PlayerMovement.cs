using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    Vector3 inputVector;
    public Transform cam;
    public bool canMove;
    public float currentSpeed;
    public float walkSpeed = 5.0f;
    public float sprintSpeed = 15.0f;
    public float stalkSpeed = 2.5f;
    public float jumpForce = 5.0f;
    public float rotationSpeed = 60.0f;

    //roll variables
    public float ButtonCooler = 0.5f; // Half a second before reset
    public int ButtonCount = 0;

    public float moveSpeedMultiplier;
    public float currentSpeedMultiplier;

    //attack variables
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
    private playerState state;

    AudioSource wolfAudio;
    public AudioClip growl;


    [SerializeField] public LayerMask groundLayer;

    enum playerState
    {
        isGrounded, isAirborn
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        radius = transform.localScale.y * 0.5f;
        canMove = true;
        moveSpeedMultiplier = 1;
        currentSpeedMultiplier = moveSpeedMultiplier;
        
        canAttack = true;

        wolfAudio = GetComponent<AudioSource>();
    }

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
                    wolfAudio.PlayOneShot(growl);
                }
            }

            if (Time.time > nextHeavyAttack)
            {
                if (Input.GetKey(KeyCode.Mouse1))
                {
                    nextHeavyAttackTimer += Time.deltaTime;
                }
                if (Input.GetKeyUp(KeyCode.Mouse1) && nextHeavyAttackTimer > 3)
                {
                    HeavyAttack();
                    Debug.Log("Player Heavy Attack");
                    wolfAudio.PlayOneShot(growl);
                }
            }
    

            if(Time.time >= nextTailWhipTime)
            {
                if(Input.GetKeyDown(KeyCode.Q))
                {
                    KnockBack();
                }
            }
        }


        //abilitie inputs
        #region
        if (Input.GetKeyDown("1"))
        {
            ability1();
        }

        if (Input.GetKeyDown("2"))
        {
            ability2();
        }

        if (Input.GetKeyDown("3"))
        {
            ability3();
        }
        #endregion
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            //roll inputs
            #region
            if (Input.GetKeyDown("a"))
            {
                RollLeft();
            }
            if (Input.GetKeyDown("w"))
            {
                DashForward();
            }
            else if (Input.GetKeyDown("d"))
            {

                RollRight();
            }
            #endregion

            if (Input.GetKey(KeyCode.LeftShift))
                currentSpeed = sprintSpeed;
            else if (Input.GetKey(KeyCode.LeftControl))
                currentSpeed = stalkSpeed;
            else
                currentSpeed = walkSpeed;

            //movement forward and back
            #region

            if (Input.GetAxis("Vertical") != 0 && state == playerState.isGrounded)
            {
                Vector3 dir = transform.forward * currentSpeed * Input.GetAxis("Vertical");
                rb.velocity = new Vector3(dir.x, rb.velocity.y, dir.z);
            }

            //Set Current Speed back to zero
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
                currentSpeed = 0.0f;
            #endregion

            //jump
            #region

            float jump = Input.GetAxis("Jump");
            
            Ray ray = new Ray(transform.position, -transform.up);
            RaycastHit info;

            if (Physics.Raycast(ray, out info, radius + 0.05f, groundLayer, QueryTriggerInteraction.Collide))
            {
                state = playerState.isGrounded;
            }
            else
            {
                state = playerState.isAirborn;
            }

            if (jump > 0 && state == playerState.isGrounded)
            {
                rb.velocity = transform.up * jumpForce;
            }
            #endregion
        }


        if(Input.GetKey(KeyCode.K))
        {

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            pushBack();
        }
    }

    private void pushBack()
    {
        transform.position -= transform.forward * currentSpeed * 0.3f;
    }




    //rolls
    #region
    private void DashForward()
    {
        if (ButtonCooler > 0 && ButtonCount == 1/*Number of Taps you want Minus One*/)
        {
            //Has double tapped
            transform.position += transform.forward * (GetComponent<PlayerMovement>().currentSpeed * 2);
        }
        else
        {
            ButtonCooler = 0.5f;
            ButtonCount += 1;
        }
    }

    private void RollLeft()
    {
        if (ButtonCooler > 0 && ButtonCount == 1/*Number of Taps you want Minus One*/)
        {
            //Has double tapped
            transform.position -= transform.right * (GetComponent<PlayerMovement>().currentSpeed * 2);
        }
        else
        {
            ButtonCooler = 0.5f;
            ButtonCount += 1;
        }
    }

    private void RollRight()
    {
        if (ButtonCooler > 0 && ButtonCount == 1/*Number of Taps you want Minus One*/)
        {
            //Has double tapped
            transform.position += transform.right * (GetComponent<PlayerMovement>().currentSpeed * 2);
        }
        else
        {
            ButtonCooler = 0.5f;
            ButtonCount += 1;
        }
    }
    #endregion

    //ability calls
    #region
    private void ability1()
    {
        Debug.Log("ability 1 was pressed");
    }

    private void ability2()
    {
        Debug.Log("ability 2 was pressed");
    }

    private void ability3()
    {
        Debug.Log("ability 3 was pressed");
    }
    #endregion

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
            //Debug.Log("Enemy Hit!");
        }
    }

    public void HeavyAttack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        // Damage the Enemy
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(heavyAttackDamge);
            //Debug.Log("Super Hit!");
        }
    }
    public void KnockBack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, knockbackRadius);
        foreach (Collider enemy in colliders)
        {
            Rigidbody rb = enemy.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(knockbackStrength, transform.position, knockbackRadius, knockUpStrength, ForceMode.Impulse);
            }
        }
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
