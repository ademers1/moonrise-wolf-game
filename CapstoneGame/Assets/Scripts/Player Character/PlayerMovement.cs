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

    public float moveSpeedMultiplier;
    public float currentSpeedMultiplier;

    private float radius;
    private playerState state;

    float icecreamfloat;

    [SerializeField] public LayerMask groundLayer;

    enum playerState
    {
        isGrounded, isAirborn
    }

    private void Awake()
    {
        GameManager.Instance.GetCamera();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        radius = transform.localScale.y * 0.5f;
        canMove = true;
        moveSpeedMultiplier = 1;
        currentSpeedMultiplier = moveSpeedMultiplier;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                currentSpeed = sprintSpeed;
            else if (Input.GetKey(KeyCode.LeftControl))
                currentSpeed = stalkSpeed;
            else
                currentSpeed = walkSpeed;

            //movement (WASD)
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
}
