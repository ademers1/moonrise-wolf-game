using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public enum AnimState
{
    isIdle, isMoving, isAttacking, isJumping, isSneaking, isDashing, isHowling, isTailwhiping
}
public class PlayerController : MonoBehaviour
{

    [FMODUnity.EventRefAttribute]
    public string pawEventString = "event:/Music/Music";

    FMOD.Studio.EventInstance pawEvent;

    FMOD.Studio.PARAMETER_ID groundQualityID;

    public Transform cam;
    public CharacterController controller;

    public float rotationSpeed = 10f;

    public float turnSmoothTime = 0.05f;
    float turnSmoothVelocity;

    public float gravity = -12;
    float velocityY;

    public float jumpHeight = 1f;

    Rigidbody rb;
    Animator anim;

    //movement values
    private float currentSpeed;
    public float walkSpeed = 6f;
    public float runSpeed = 10f;
    public float stalkSpeed = 2.5f;
    public float jumpForce = 100.0f;
    private float t = 0;//this variable will be discontinued with animation events


    public float howlCooldownAmount = 5.0f;
    public float howlTimer = 0;
    public float howlRadius = 10f;
    private bool howlOnCooldown;
    private LayerMask enemies;

    private Vector3 rightFootPosition, leftFootPosition, leftFootIkPosiiton, rightFootIkPosition;
    private Quaternion leftFootIkRotation, rightFootIkRotation;
    private float lastPelvisPositionY, lastRightFootPositionY, lastLeftFootPositionY;

    [Header("Feet Grounder")]
    public bool enableFeetIk = true;
    [Range(0, 2)] [SerializeField] private float heightFromGroundRaycast = 1.14f;
    [Range(0, 2)] [SerializeField] private float raycastDownDistance = 1.5f;
    [SerializeField] private LayerMask environmentLayer;
    [SerializeField] private float pelvisOffset = 0f;
    [Range(0, 1)] [SerializeField] private float pelvisUpAndDownSpeed = 0.28f;
    [Range(0, 1)] [SerializeField] private float feetToIkPositionSpeed = 0.5f;

    public string leftFoodAnimVariableName = "LeftFootCurve";
    public string rightFoodAnimVariableName = "RightFootCurve";

    public bool useProIkFeature = false;
    public bool showSolverDebug = true;
    public bool InvisBox = false;

    [Header("Attacks")]
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
    public AnimState animState
    {
        get
        {
            return _animState;
        }
    }

    //dash variables
    private int dashesRemaining = 3;
    public float dashChargeTime = 3.0f;
    public float dashTimer = 0f;
    public float dashLength = 0.15f;
    public float dashSpeed = 100f;
    public BoxCollider collider;
    
    private Vector3 dashMove;
    private float dashing = 0f;
    private float dashingTime = 0f;
    private bool canDash = true;
    private bool dashReset = true;
    Collider[] dashCollider;

    Vector3 velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        enemies = LayerMask.GetMask("Enemies");
        //furyBar.fillAmount = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //controller = GetComponent<CharacterController>();

        
    }

    // Update is called once per frame

    private void Update()
    {

        velocityY += Time.deltaTime * gravity;

        //if we're not moving we need to constantly check if we're groudned and if not fall
        if (_animState != AnimState.isMoving)
        {

            if (controller.isGrounded)
            {
                velocityY = 0;
            }
            else
            {
                velocity = Vector3.zero + Vector3.up * velocityY;
                controller.Move(velocity * Time.deltaTime);
            }
        }

        //basic movement
        if (_animState == AnimState.isIdle || _animState == AnimState.isMoving || _animState == AnimState.isSneaking)
        {

            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            t = 0;

            if (Input.GetButtonDown("Jump"))
                Jump();


            if (direction.magnitude >= 0.1f)
            {
                _animState = AnimState.isMoving;
                //get the angle we move and change the camera to match that angle

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                //transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

                bool running = Input.GetKey(KeyCode.LeftShift);
                bool sneaking = Input.GetKey(KeyCode.LeftControl);
                float currentSpeed = ((running) ? runSpeed : walkSpeed) * direction.magnitude;
                float sneakSpeed = ((sneaking) ? stalkSpeed : walkSpeed) * direction.magnitude;

                if(sneaking)
                {
                    _animState = AnimState.isSneaking;
                    currentSpeed = sneakSpeed;
                }

                velocityY += Time.deltaTime * gravity;

                velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

                controller.Move(velocity * Time.deltaTime);

            
                if (controller.isGrounded)
                {
                    velocityY = 0;
                }
            }

            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", true);
        }

        //dash
        if (_animState == AnimState.isIdle || _animState == AnimState.isMoving || _animState == AnimState.isSneaking && dashesRemaining > 0)
        {
            //left ctrl or left mouse button
            if (Input.GetKeyDown(KeyCode.Z) && canDash && dashesRemaining >= 1)
            {
                dashMove = velocity.normalized;
                canDash = false;
                _animState = AnimState.isDashing;
                dashReset = false;
                dashesRemaining--;
                dashCollider = Physics.OverlapSphere(transform.position, tailWhipRadius, enemies);
                foreach (Collider col in dashCollider)
                {
                    col.isTrigger = true;
                }
            }


        }

        if (!canDash && dashing < dashLength)
        {
            controller.Move(dashMove * dashSpeed * Time.deltaTime);
            dashing += Time.deltaTime;
        }


        if (controller.isGrounded && canDash == false && dashing >= dashLength)
        {
            _animState = AnimState.isIdle;
            canDash = true;
            dashing = 0f;
            foreach (Collider col in dashCollider)
            {
                col.isTrigger = false;
            }
        }

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
                Collider[] colliders = Physics.OverlapSphere(transform.position, howlRadius, enemies);
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
                Collider[] colliders = Physics.OverlapSphere(transform.position, tailWhipRadius, enemies);
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
            if (tailWhipTimer >= tailWhipCooldownAmount)
            {
                tailWhipTimer = 0;
                tailWhipOnCooldown = false;
            }
        }
    }


    private void Jump()
    {
        if (controller.isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
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
            enemy.GetComponent<NPCHealth>().Health -= attackDamage;
        }
    }

    public void HeavyAttack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemies);
        // Damage the Enemy
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<NPCHealth>().Health -= heavyAttackDamage;
        }
    }

    private void Knockback(Collider[] colliders)
    {
        foreach (Collider enemy in colliders)
        {
            Vector3 enemyKnockBackDirection = enemy.transform.position - transform.position;
            enemyKnockBackDirection.y = 0;
            enemyKnockBackDirection.Normalize();
            enemy.GetComponent<Rigidbody>().AddForce(enemyKnockBackDirection * 10, ForceMode.Impulse);
        }
        StartCoroutine(ReAddKinematic(colliders));
    }

    private IEnumerator ReAddKinematic(Collider[] colliders)
    {
        yield return new WaitForSeconds(1.0f);
        foreach (Collider enemy in colliders)
        {
            enemy.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("InvisBox"))
        {
            InvisBox = true;
          //  _animState = AnimState.isSneaking;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("InvisBox"))
        {
            InvisBox = false;
           // _animState = AnimState.isIdle;
        }
    }

    public void PawStrikeSound()
    {
        //Audio
        pawEvent = FMODUnity.RuntimeManager.CreateInstance(pawEventString);
        FMOD.Studio.PARAMETER_DESCRIPTION groundDesc;
        FMOD.Studio.EventDescription pawDesc;

        pawEvent.getDescription(out pawDesc);
        pawDesc.getParameterDescriptionByName("Ground Quality", out groundDesc);
        groundQualityID = groundDesc.id;
        pawEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform));

        pawEvent.setParameterByID(groundQualityID, 50);

        pawEvent.start();
        pawEvent.release();
    }
}
