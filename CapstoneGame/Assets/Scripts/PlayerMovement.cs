using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 0.0f;
    public float jumpForce = 5.0f;
    public float rotationSpeed = 60.0f;

    private float radius;
    private playerState state;
    public bool isoCam = false;
    public bool thirdCam = false;
    public bool firstCam = false;

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
        float jump = Input.GetAxis("Jump");
        if (thirdCam)
        {
            if (Input.GetKey(KeyCode.W))
            {
                //rb.velocity = Vector3.forward * speed * Time.deltaTime);
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            //float moveHorizontal = Input.GetAxis("Horizontal");
            //float moveVertical = Input.GetAxis("Vertical");
            
            //rb.AddForce(new Vector3(moveHorizontal, 0.0f, moveVertical) * speed);
        }
        if (isoCam)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * speed * Time.deltaTime);
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


    }
}
