using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skilltree : MonoBehaviour
{
    private List<Skill> skills = new List<Skill>();

    Skill moveSpeed = new Skill(2, 1, 1, 1, false, "Move Speed");
    Skill jump = new Skill(1, 1, 2, 1, false, "High Jump");
    Skill healthRegen = new Skill(1,2,1,1, false, "Health Regen");
    Skill levelHealth = new Skill(1, 1, 1, 2, false, "Hp Increase");
    Skill bloodLust = new Skill(1, 1, 1, 1, true, "Bloodlust");

    DirectedGraph<Skill> skillTree;

    
    private void Start()
    {
        skills.Add(moveSpeed);
        skills.Add(jump);
        skills.Add(healthRegen);
        skills.Add(levelHealth);
        skills.Add(bloodLust);
        skillTree = new DirectedGraph<Skill>(skills);

        skillTree.AddEdge(moveSpeed, jump);
        skillTree.AddEdge(moveSpeed, healthRegen);
        skillTree.AddEdge(jump, bloodLust);
        skillTree.AddEdge(healthRegen, levelHealth);
    }
}
