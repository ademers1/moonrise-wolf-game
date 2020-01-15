using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Skilltree skilltree = new Skilltree();
    
    
    private void Start()
    {
        skilltree.CreateTree();

    }
}
