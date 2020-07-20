using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceDisplay : MonoBehaviour
{
    public Image ExperienceBar;
    public Text LevelText;

    // Start is called before the first frame update
    void Start()
    {
        LevelText.text = "Level: 1";
    }

    public void UpdateExperience(float xpToLevel, float xp)
    {
        ExperienceBar.fillAmount = xp / xpToLevel;
    }

    public void LevelUp(int level)
    {
        LevelText.text = "Level: " + level;
    }
}
