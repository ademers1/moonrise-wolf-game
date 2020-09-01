using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealth : Killable
{
    public float _health;
    public Vector3 enemyCurrentPosition;
    private void Start()
    {
        MaxHealth = 100;
        Health = 100;

    }

    // Update is called once per frame
    void Update()
    {
        _health = Health;
        enemyCurrentPosition = transform.position;
        
    }
}
