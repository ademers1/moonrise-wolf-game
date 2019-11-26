
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 10f;

    Vector3 dir;
    float distanceThisFrame;

    Rigidbody rb;
    public float force = 2;


    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        dir = target.position - transform.position;
        distanceThisFrame = speed * Time.deltaTime;

        //transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        rb.AddForce(dir * force, ForceMode.Acceleration);
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        
        /*
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        } */
    }

    void HitTarget()
    {
        target.GetComponent<CharacterHealth>().TakeDamage(25);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            target.GetComponent<CharacterHealth>().TakeDamage(25);
        }

        Debug.Log("hit");
        Destroy(gameObject);
    }

}

