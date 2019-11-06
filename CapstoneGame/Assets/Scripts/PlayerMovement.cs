using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 20.0f;
    public float jumpForce = 5.0f;

    private float radius;
    private playerState state;

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
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float jump = Input.GetAxis("Jump");
        rb.AddForce(new Vector3(moveHorizontal, 0.0f, moveVertical) * speed);

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
