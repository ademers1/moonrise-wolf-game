using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour, IPointerClickHandler
{
    public WolfLevel level;
    public bool Attack;
    public bool Health;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Attack)
        {
            level.LevelUpAttack();
        }
        if (Health)
        {
            level.LevelUpHealth();
        }
    }

}
