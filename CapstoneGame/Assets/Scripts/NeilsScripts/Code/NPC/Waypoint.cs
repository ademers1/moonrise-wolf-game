using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    protected float debugDrawRadius = 1.0f;

    public virtual void OnDrawGizmo()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);
    }
}
