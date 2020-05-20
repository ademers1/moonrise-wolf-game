using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] int enemyStatus;
    StunParticals stunParticalsScript;
    bool tranquilized;
    // Start is called before the first frame update
    void Start()
    {
        stunParticalsScript = GetComponent<StunParticals>();
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
        yield return new WaitForSeconds(effectTime);
        stunParticalsScript.endEmit(stunParticalsScript.stunParticalLauncher);
    }
    
}
