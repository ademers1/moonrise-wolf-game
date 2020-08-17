using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class VFXController : MonoBehaviour
{
    [SerializeField] VisualEffect visualEffect;
    [SerializeField] Vector3 target;
    [SerializeField] Vector3 start;
    WolfSense wolfSenseScript;
    GameObject Player;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        wolfSenseScript = Player.GetComponent<WolfSense>();
    }

    // Update is called once per frame
    void Update()
    {
        //follows the player
        this.gameObject.transform.position = Player.transform.position;
        //if wolf sense is on
        if(wolfSenseScript.WolfSenseOn)
        {
            start = Player.transform.position;
            target = wolfSenseScript.ClosestEnemy.transform.position;

            if(target != null && start != null)
            {
                //set target
                visualEffect.SetVector3("Target", target);
                //set start point
                visualEffect.SetVector3("Start", start);
                visualEffect.Play();
            }
        }
        else
        {
            visualEffect.Stop();
        }
    }
}
