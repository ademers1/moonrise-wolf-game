using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem 
{
    private int level;
    private int currentExperience;
    private int totalExperience;
    private int experienceToNextLevel;

    public LevelSystem()
    {
        level = 0;
        currentExperience = 0;
        experienceToNextLevel = 100;
    }

    public void AddExperience(int amount)
    {
        currentExperience += amount;
        totalExperience += amount;
        if(currentExperience >= experienceToNextLevel)
        {
            level++;
            currentExperience -= experienceToNextLevel;
        }
    }
}
