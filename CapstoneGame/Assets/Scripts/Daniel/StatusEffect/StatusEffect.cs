using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{
    int playerStatus;
    //list of status effects
    public enum statusEffects
    {
        Stunned = 0,
        Slowed = 1,
        Rooted = 2,
        Tranquilized = 3,
        Zapped = 4,
        Engulfed = 5
    }    
    //weapon has status effect property will call this function
    public void Effect(int e)
    {
        playerStatus = e;
        switch (playerStatus)
        {
            case 0:
                Debug.Log(statusEffects.Stunned);
                break;
            case 1:
                Debug.Log(statusEffects.Slowed);
                break;
            case 2:
                Debug.Log(statusEffects.Rooted);
                break;
            case 3:
                Debug.Log(statusEffects.Tranquilized);
                break;
            case 4:
                Debug.Log(statusEffects.Zapped);
                break;
            case 5:
                Debug.Log(statusEffects.Engulfed);
                break;
        }
    }
}
