using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBlockage : MonoBehaviour
{
    float wallMoveDistance = 15.0f;
    public List<GameObject> EnemiesToDefeat;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(EnemiesToDefeat.Count == 0)
        {
            Destroy(this.gameObject);
        }
    }

   
}
