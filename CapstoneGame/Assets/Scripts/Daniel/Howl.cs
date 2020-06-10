using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Howl : MonoBehaviour
{
    [SerializeField] private float howlRadius;
    [SerializeField] private float howlCooldown;
    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {
        howlRadius = 10.0f;
        howlCooldown = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= howlCooldown)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                WolfHowl();
            }
        }
    }
    public void WolfHowl()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, howlRadius, enemyLayers);
        foreach (Collider enemy in colliders)
        {
            if (enemy.tag != "Player")
            {
                enemy.GetComponent<EnemyStatus>().Effect(0);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, howlRadius);
    }
}
