using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    Animator anim;

    //roll variables
    public float ButtonCooler = 0.5f; // Half a second before reset
    public int ButtonCount = 0;

    public float moveSpeedMultiplier;
    public float currentSpeedMultiplier;


    private float radius;
    private PlayerJumpState state;
    private PlayerAnimationState animState;


    [SerializeField] public LayerMask groundLayer;

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

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        radius = transform.localScale.y * 0.5f;
        canMove = true;
        moveSpeedMultiplier = 1;
        currentSpeedMultiplier = moveSpeedMultiplier;      
    }

    void Update()
    {
        GameManager.Instance.PlayGameMusic();
        if (Input.GetAxis("Vertical") == 0 && animState == PlayerAnimationState.isMoving)
        {
            animState = PlayerAnimationState.isIdle;
            anim.SetBool("isIdle", true);
            anim.SetBool("isRunning", false);
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

            if (Input.GetAxis("Vertical") != 0 && state == PlayerJumpState.isGrounded && (animState == PlayerAnimationState.isIdle || animState == PlayerAnimationState.isMoving))
            {
                Vector3 dir = transform.forward * currentSpeed * Input.GetAxis("Vertical");
                rb.velocity = new Vector3(dir.x, rb.velocity.y, dir.z);
                animState = PlayerAnimationState.isMoving;
                anim.SetBool("isIdle", false);
                anim.SetBool("isRunning", true);
            }

            //Set Current Speed back to zero
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
                currentSpeed = 0.0f;
            #endregion

            //jump
            #region

            float jump = Input.GetAxis("Jump");
            
            Ray ray = new Ray(transform.position + new Vector3(0, 0.3f, 0), -transform.up);
            RaycastHit info;

            if (Physics.Raycast(ray, out info, radius + 0.05f, groundLayer, QueryTriggerInteraction.Collide))
            {
                state = PlayerJumpState.isGrounded;
            }
            else
            {
                state = PlayerJumpState.isAirborn;
            }

            if (jump > 0 && state == PlayerJumpState.isGrounded)
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

  
}
