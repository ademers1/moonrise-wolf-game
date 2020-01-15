using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkill : Skill
{
    private Stat _stat;
    private int _percentModifier;
    private float _cooldown;
    private float _length;
   
    public Stat Stat
    {
        get { return _stat; }       
    }

    public PassiveSkill(Stat stat, int mod, float cd, float length, string name) : base(name) {
        _stat = stat;
        _percentModifier = mod;
        _cooldown = cd;
        _length = length;
    }

    public PassiveSkill(Stat stat, int mod, string name) : base(name)
    {
        _stat = stat;
        _percentModifier = mod;
        _cooldown = -1;
        _length = -1;
    }
}
