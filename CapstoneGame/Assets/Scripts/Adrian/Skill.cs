using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    private float moveModifier;
    private float healthRegen;
    private float jumpHeight;
    private float levelHealth;
    private bool bloodLust;
    private string name;

    public Skill(int move, int regen, int jump, int health, bool lust, string name) 
    {
        moveModifier = move;
        healthRegen = regen;
        jumpHeight = jump;
        levelHealth = health;
        bloodLust = lust;
        this.name = name;
    }


    public float GetMoveModifier()
    {
        return moveModifier;
    }
    public float GetHealthRegen()
    {
        return healthRegen;
    }
    public float GetJumpHeight()
    {
        return jumpHeight;
    }
    public float GetLevelHealth()
    {
        return levelHealth;
    }
    public bool GetBloodlust()
    {
        return bloodLust;
    }
    public string GetName()
    {
        return name;
    }
}
