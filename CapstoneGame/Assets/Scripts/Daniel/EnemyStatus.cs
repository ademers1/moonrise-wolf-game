using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] int enemyStatus;
    StunParticals stunParticalsScript;
    RangedEnemyPatrol partrolScript;
    EnemyContol enemyScript;
    bool tranquilized;
    // Start is called before the first frame update
    void Start()
    {
        stunParticalsScript = GetComponent<StunParticals>();
        partrolScript = GetComponent<RangedEnemyPatrol>();
        enemyScript = GetComponent<EnemyContol>();
    }
    public enum statusEffects
    {
        //can't move or attack
        Stunned = 0,
        defaultStatus = 1
    }
    public void Effect(int e)
    {
        enemyStatus = e;
        switch (enemyStatus)
        {
            case 0:
                Debug.Log(statusEffects.Stunned);
                StartCoroutine(Stun());
                break;
            case 1:
                Debug.Log("default Status");
                break;
        }
    }
    // Update is called once per frame
    IEnumerator Stun()
    {
        float effectTime = 2f;
        stunParticalsScript.startEmit(stunParticalsScript.stunParticalLauncher);
        if (partrolScript != null) {
            partrolScript.enabled = false;
        }
        if (enemyScript != null)
        {
            enemyScript.enabled = false;
        }
        yield return new WaitForSeconds(effectTime);
        stunParticalsScript.endEmit(stunParticalsScript.stunParticalLauncher);
        if (partrolScript != null)
        {
            partrolScript.enabled = true;
        }
        if (enemyScript != null)
        {
            enemyScript.enabled = true;
        }
    }
    
}
