using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stat { Hitpoints = 0, Attack = 1, Defense = 2, AttackSpeed = 3, HealthRegen = 4, MoveSpeed = 5, JumpHeight = 6 }

public abstract class Skill
{
    private readonly string _name;
    private readonly bool _active;

    public string Name
    {
        get { return _name; }
    }

    public bool Active
    {
        get { return _active;  }
    }

    public Skill(string name)
    {
        _name = name;
        _active = false;
    }
}