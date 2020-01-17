using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skilltree
{
    private List<Skill> skills = new List<Skill>();

    PassiveSkill swiftStride = new PassiveSkill(Stat.MoveSpeed, 100, "Swift Stride");
    PassiveSkill naturalHealing = new PassiveSkill(Stat.HealthRegen, 5, "Natural Healing");
    PassiveSkill augmentedLegs = new PassiveSkill(Stat.JumpHeight, 100, "Augmented Legs");
    PassiveSkill ironHide = new PassiveSkill(Stat.Defense, 25, "Ironhide");
    PassiveSkill bloodlust = new PassiveSkill(Stat.Attack, 100, 60, 5, "Bloodlust");

    DirectedGraph<Skill> skillTree;

    public DirectedGraph<Skill> SkillTree
    {
        get { return skillTree; }
    }

    public void CreateTree()
    {
        skills.Add(swiftStride);
        skills.Add(naturalHealing);
        skills.Add(ironHide);
        skills.Add(augmentedLegs);
        skills.Add(bloodlust);
        skillTree = new DirectedGraph<Skill>(skills);

        skillTree.AddEdge(swiftStride, naturalHealing);
        skillTree.AddEdge(swiftStride, ironHide);
        skillTree.AddEdge(ironHide, bloodlust);
        skillTree.AddEdge(naturalHealing, augmentedLegs);
    }
    
    
}