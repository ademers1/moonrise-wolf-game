using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{
    [SerializeField] int playerStatus;
    PlayerMovement playerMovementScript;
    StunParticals stunParticalsScript;
    bool tranquilized;
    private void Start()
    {
        playerMovementScript = GetComponent<PlayerMovement>();
        stunParticalsScript = GetComponent<StunParticals>();
    }
    //list of status effects
    public enum statusEffects
    {
        //can't move or attack
        Stunned = 0,
        //slow movement speed
        Slowed = 1,
        //can't move, can attack
        Rooted = 2,
        //slowed, and increase speed overtime
        Tranquilized = 3,
        //zap 3 times
        Zapped = 4,
        Engulfed = 5,
        defaultStatus = 6
    }    
    //weapon has status effect property will call this function
    public void Effect(int e)
    {
        playerStatus = e;
        switch (playerStatus)
        {
            case 0:
                Debug.Log(statusEffects.Stunned);
                StartCoroutine(Stun());
                break;
            case 1:
                Debug.Log(statusEffects.Slowed);
                StartCoroutine(Slow());
                break;
            case 2:
                Debug.Log(statusEffects.Rooted);
                StartCoroutine(Root());
                break;
            case 3:
                Debug.Log(statusEffects.Tranquilized);
                tranquilized = true;
                playerMovementScript.currentSpeedMultiplier = 0.01f;
                StartCoroutine(Tranquilized());
                break;
            case 4:
                Debug.Log(statusEffects.Zapped);
                StartCoroutine(Zapped());
                break;
            case 5:
                Debug.Log(statusEffects.Engulfed);
                break;
            case 6:
                Debug.Log("default Status");
                break;
        }
    }
    private void Update()
    {
        if(tranquilized)
        {
            if(playerMovementScript.currentSpeedMultiplier<playerMovementScript.moveSpeedMultiplier)
            {
                playerMovementScript.currentSpeedMultiplier += ((playerMovementScript.moveSpeedMultiplier - playerMovementScript.currentSpeedMultiplier) * Time.deltaTime) / 3;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="stun")
        {
            Effect(0);
        }
        if (collision.gameObject.tag == "slow")
        {
            Effect(1);
        }
        if (collision.gameObject.tag == "root")
        {
            Effect(2);
        }
        if (collision.gameObject.tag == "tranquilized")
        {
            Effect(3);
        }
        if (collision.gameObject.tag == "zapped")
        {
            Effect(4);
        }
    }
    IEnumerator Stun()
    {
        float effectTime = 2f;
        playerMovementScript.canMove = false;
        playerMovementScript.canAttack = false;
        stunParticalsScript.startEmit(stunParticalsScript.stunParticalLauncher);
        yield return new WaitForSeconds(effectTime);
        playerMovementScript.canMove = true;
        playerMovementScript.canAttack = true;
        stunParticalsScript.endEmit(stunParticalsScript.stunParticalLauncher);
    }
    IEnumerator Slow()
    {
        float effectTime = 2f;
        playerMovementScript.currentSpeedMultiplier = 0.3f;
        stunParticalsScript.startEmit(stunParticalsScript.slowParticalLauncher);
        yield return new WaitForSeconds(effectTime);
        stunParticalsScript.endEmit(stunParticalsScript.slowParticalLauncher);
        playerMovementScript.currentSpeedMultiplier = 1f;
    }
    IEnumerator Root()
    {
        float effectTime = 2f;
        playerMovementScript.canMove = false;
        stunParticalsScript.startEmit(stunParticalsScript.rootParticalLauncher);
        yield return new WaitForSeconds(effectTime);
        stunParticalsScript.endEmit(stunParticalsScript.rootParticalLauncher);
        playerMovementScript.canMove = true;
    }
    //zap 3 times
    IEnumerator Zapped()
    {
        float zapCooldown = 0.3f;
        playerMovementScript.canMove = false;
        playerMovementScript.canAttack = false;
        stunParticalsScript.startEmit(stunParticalsScript.zapParticalLauncher);
        yield return new WaitForSeconds(zapCooldown);
        playerMovementScript.canMove = true;
        playerMovementScript.canAttack = true;
        yield return new WaitForSeconds(zapCooldown);
        playerMovementScript.canMove = false;
        playerMovementScript.canAttack = false;
        stunParticalsScript.startEmit(stunParticalsScript.zapParticalLauncher);
        yield return new WaitForSeconds(zapCooldown);
        playerMovementScript.canMove = true;
        playerMovementScript.canAttack = true;
        yield return new WaitForSeconds(zapCooldown);
        playerMovementScript.canMove = false;
        playerMovementScript.canAttack = false;
        stunParticalsScript.startEmit(stunParticalsScript.zapParticalLauncher);
        yield return new WaitForSeconds(zapCooldown);
        stunParticalsScript.endEmit(stunParticalsScript.zapParticalLauncher);
        playerMovementScript.canMove = true;
        playerMovementScript.canAttack = true;
    }
    IEnumerator Tranquilized()
    {
        float effectTime = 3f;
        stunParticalsScript.startEmit(stunParticalsScript.slowParticalLauncher);
        yield return new WaitForSeconds(effectTime);
        stunParticalsScript.endEmit(stunParticalsScript.slowParticalLauncher);
    }
}
