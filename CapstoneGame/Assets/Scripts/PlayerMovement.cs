using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    public float currentSpeed;
    public float walkSpeed = 5.0f;
    public float sprintSpeed = 15.0f;

    public float jumpForce = 5.0f;
    public float rotationSpeed = 60.0f;

    private float radius;
    private playerState state;
    public bool isoCam = false;
    public bool thirdCam = false;
    public bool firstCam = false;

    float icecreamfloat;

    [SerializeField] private LayerMask groundLayer;

    enum playerState
    {
        isGrounded, isAirborn
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        radius = transform.localScale.y * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey(KeyCode.LeftShift)) 
            currentSpeed = sprintSpeed;
        else
            currentSpeed = walkSpeed;

        //movement (WASD)
        #region
        if (Input.GetKey(KeyCode.W))
            transform.position += transform.forward * currentSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            transform.position -= transform.forward * (currentSpeed * 0.85f) * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            transform.position -= transform.right * (currentSpeed * 0.85f) * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            transform.position += transform.right * (currentSpeed * 0.85f) * Time.deltaTime;

        //Set Current Speed back to zero
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            currentSpeed = 0.0f;
        #endregion

        //jump
        #region

        float jump = Input.GetAxis("Jump");
        if (thirdCam)
        {
            if (Input.GetKey(KeyCode.W))
            {
                //rb.velocity = Vector3.forward * speed * Time.deltaTime);
                transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
            }
            //float moveHorizontal = Input.GetAxis("Horizontal");
            //float moveVertical = Input.GetAxis("Vertical");
            
            //rb.AddForce(new Vector3(moveHorizontal, 0.0f, moveVertical) * speed);
        }
        if (isoCam)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
            }
            //float moveHorizontal = Input.GetAxis("Horizontal");
            //float moveVertical = Input.GetAxis("Vertical");
            //rb.AddForce(new Vector3(moveHorizontal, 0.0f, moveVertical) * speed);
        }
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit info;

        if(Physics.Raycast(ray, out info, radius + 0.05f, groundLayer, QueryTriggerInteraction.Collide))
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
}
