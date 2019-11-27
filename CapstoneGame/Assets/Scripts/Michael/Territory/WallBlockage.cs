using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBlockage : MonoBehaviour
{
    GameObject wall;
    float wallMoveDistance = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        wall = GameObject.Find("Wall Blockage");

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Player"))
        { 
            wall.transform.position += wall.transform.forward * wallMoveDistance;
            Destroy(gameObject);
        }
    }
}
