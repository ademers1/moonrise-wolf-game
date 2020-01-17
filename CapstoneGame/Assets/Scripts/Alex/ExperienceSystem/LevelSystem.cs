using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem 
{
    private int level;
    private int levelsHeld;
    private int actualExperience;
    private int currentExperience;
    private int totalExperience;

    private int experienceToNextLevel;

    public int attackLvl = 0;
    public int healthlvl = 0;
    List<int> levelCurve = new List<int>();
    int currentLevel = 0;
    

    public LevelSystem()
    {
        level = 1;
        levelsHeld = 0;
        currentExperience = 0;
        experienceToNextLevel = 100;
        levelCurve.Add(experienceToNextLevel);
    }

    public void AddExperience(int amount, ExperienceDisplay experienceDisplay)
    {
        currentExperience += amount;
        totalExperience += amount;
        actualExperience += amount;
        experienceDisplay.UpdateExperience(experienceToNextLevel, currentExperience);
        if (actualExperience >= experienceToNextLevel)
        {
            levelsHeld++;
            experienceDisplay.LevelUp(level);
            actualExperience -= experienceToNextLevel;
            levelCurve.Add(experienceToNextLevel);
        }
        if (currentExperience <= experienceToNextLevel)
        {
            experienceDisplay.UpdateExperience(experienceToNextLevel, currentExperience);
        }
    }

    public int GetLevelsHeld()
    {
        return levelsHeld;
    }

    public void LevelUpAttack(ExperienceDisplay experienceDisplay)
    {
        attackLvl++;
        level++;
        levelsHeld--;
        if (levelsHeld < 0)
        { 
            levelsHeld = 0;
        }
        currentExperience -= levelCurve[currentLevel];
        currentLevel++;
        experienceDisplay.UpdateExperience(experienceToNextLevel, currentExperience);
    }
    public void LevelUpHealth(ExperienceDisplay experienceDisplay)
    {
        healthlvl++;
        level++;
        levelsHeld--;
        if (levelsHeld < 0)
        {
            levelsHeld = 0;
        }
        currentExperience -= levelCurve[currentLevel];
        currentLevel++;
        experienceDisplay.UpdateExperience(experienceToNextLevel, currentExperience);
    }
}
