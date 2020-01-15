using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WolfLevel : MonoBehaviour
{
    LevelSystem level = new LevelSystem();
    ExperienceDisplay experienceDisplay;
    public Image AttackUp;
    public Image HealthUp;
    public PlayerAttack playerAttack;
    public CharacterHealth playerHealth;
    bool isLeveling;
    public InventorySystem IS;

    private void Start()
    {
        experienceDisplay = GetComponent<ExperienceDisplay>();
    }

    private void Update()
    {       
        if(level.GetLevelsHeld() > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("SkillTree");
            }
        }
    }

    public void AddExperience(int amount)
    {
        level.AddExperience(amount, experienceDisplay);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Den")
        {
            Debug.Log(level.GetLevelsHeld());
            if(level.GetLevelsHeld() > 0)
            {
                ShowOptions();
            }
            else
            {
                HideOptions();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Den")
        {
            HideOptions();
        }
    }

    void ShowOptions()
    {
        AttackUp.gameObject.SetActive(true);
        HealthUp.gameObject.SetActive(true);
        isLeveling = true;
    }

    void HideOptions()
    {
        AttackUp.gameObject.SetActive(false);
        HealthUp.gameObject.SetActive(false);
        isLeveling = false;
    }

    public void LevelUpAttack()
    {
        level.LevelUpAttack(experienceDisplay);
        playerAttack.attackDamage += 1;
    }

    public void LevelUpHealth()
    {
        level.LevelUpHealth(experienceDisplay);
        playerHealth.startHealth += 10;
        playerHealth.health += 10;        
    }
}
