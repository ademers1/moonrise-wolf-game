using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum AnimState
{
    isIdle, isMoving, isAttacking, isJumping, isSneaking, isDashing, isHowling, isTailwhiping
}
public class PlayerController : MonoBehaviour
{

    public Transform cam;

    public float speed = 6f;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    Rigidbody rb;
    Animator anim;

    //movement values
    float currentSpeed;
    public float walkSpeed = 6f;
    public float runSpeed = 10f;
    public float stalkSpeed = 2.5f;
    public float jumpForce = 100.0f;
    public float dashForce = 1000f;
    float t = 0;

    private int dashesRemaining = 3;
    public float dashChargeTime = 3.0f;
    public float dashTimer = 0f;

    public float howlCooldownAmount = 5.0f;
    public float howlTimer = 0;
    public float howlRadius = 10f;
    private bool howlOnCooldown;
    private LayerMask enemies;

    Vector3 moveDir = new Vector3(0,0,1);

    private AnimState _animState = AnimState.isIdle;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        enemies = LayerMask.GetMask("Enemy");
    }

    // Update is called once per frame

    private void Update()
    {
        //recharge dashes if we don't currently have 3
        if(dashesRemaining < 3)
        {
            dashTimer += Time.deltaTime;
            if(dashTimer > dashChargeTime)
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

        if(_animState == AnimState.isJumping)
        {
            if (isGrounded())
            {
                _animState = AnimState.isIdle;
            }
        }

        if(_animState == AnimState.isIdle || _animState == AnimState.isMoving || _animState == AnimState.isSneaking)
        {
            if (Input.GetKey("Fire2"))
            {
                _animState = AnimState.isHowling;
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
        if(howlOnCooldown)
        {
            howlTimer += Time.deltaTime;
            if(howlTimer >= howlCooldownAmount)
            {
                howlOnCooldown = false;
                howlTimer = 0f;
            }
        }
    }
    void FixedUpdate()
    {

        if (_animState == AnimState.isIdle || _animState == AnimState.isMoving)
        {
            
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
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
                rb.velocity = moveDir.normalized * speed;
            }

            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", true);
        }

        if(_animState == AnimState.isIdle || _animState == AnimState.isMoving || _animState == AnimState.isSneaking && dashesRemaining > 0)
        {
            //left ctrl or left mouse button
            if (Input.GetButtonDown("Fire1"))
            {
                dashesRemaining--;
                _animState = AnimState.isDashing;
                Vector3 dashDirection;
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                if(vertical != 0 || horizontal == 0)
                {
                    //if a key isn't held down 
                    //or forward/back is held down
                    //then we want to dash straight ahead
                    horizontal = 0;
                    dashDirection = moveDir;
                    Debug.Log(dashDirection);
                    rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);
                }
                else
                {
                    //else we want to dash to the side
                    //currently not working as we start moving to the side first
                    //TODO: figure out a new key to press for sideways dashes
                    if(horizontal < 0)
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

        if(_animState == AnimState.isMoving && isGrounded())
        {
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log(isGrounded());
                _animState = AnimState.isJumping;
                rb.velocity = rb.velocity + Vector3.up * jumpForce;
            }
        }
        
    }


    private bool isGrounded()
    {
        RaycastHit ray;
        LayerMask ground = LayerMask.GetMask("Ground");
        if(Physics.Raycast(transform.position + new Vector3(0, 0.3f, 0), Vector3.down, out ray, .4f, ground))
        {
            return true;
        }
        return false;
    }
}
