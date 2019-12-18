using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointScript : MonoBehaviour
{
    public Transform Raycastpoint;
    public LayerMask mask;
    private Rigidbody rb;
    private void Awake()
    {

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            OnRightClick();
        }
    }

    void OnRightClick()
    {
        Ray ray = new Ray(Raycastpoint.position, Raycastpoint.forward);
        RaycastHit info;

        if(Physics.Raycast(ray, out info, 3.5f, mask, QueryTriggerInteraction.Ignore))
        {
            info.collider.gameObject.AddComponent<SpringJoint>();
            info.collider.gameObject.GetComponent<SpringJoint>().connectedBody = rb;
            Debug.Log("Created Joint");
            Debug.DrawLine(ray.origin, info.point, Color.red);
        }

       
    }
}
