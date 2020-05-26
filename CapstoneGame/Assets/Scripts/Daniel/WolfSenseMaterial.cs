using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSenseMaterial : MonoBehaviour
{
    public Material[] material;
    public Renderer rend;
    public bool beingSensed;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
        beingSensed = false;
    }
    private void Update()
    {
        if(beingSensed == true && rend.sharedMaterial != material[1])
        {
            rend.sharedMaterial = material[1];
        }
        else if (beingSensed == false && rend.sharedMaterial != material[0])
        {
            rend.sharedMaterial = material[0];
        }
    }

}
