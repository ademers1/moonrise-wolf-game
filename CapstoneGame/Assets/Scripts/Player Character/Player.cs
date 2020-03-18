using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    Skilltree skilltree = new Skilltree();
    public Canvas visualTree;
    bool open;
    public Button[] skillButtons;
    List<Skill> skills;
    public int skillPoints;
    public Text skillPointText;
    
    private void Start()
    {      
        skilltree.CreateTree();
        skills = skilltree.SkillTree.GetNodes() as List<Skill>;
        PopulateSkillNames();
        SetActiveButtons();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            open = !open;
            if (open)
            {
                visualTree.gameObject.SetActive(true);
                GameManager.Instance.Camera.ShowMouse();
            }
            else
            {
                visualTree.gameObject.SetActive(false);
                GameManager.Instance.Camera.HideMouse();
            }
        }
    }

    private void PopulateSkillNames()
    {
        
        for(int i = 0; i < skills.Count; i++)
        {
            skillButtons[i].GetComponentInChildren<Text>().text = skills[i].Name;
            skillButtons[i].onClick.AddListener(SkillClicked);
        }
    }

    private void SetActiveButtons()
    {       
        for(int i = 1; i < skillButtons.Length; i++)
        {
            skillButtons[i].interactable = false;
        }

    }

    private void SkillClicked()
    {
        if (skillPoints > 0)
        {
            --skillPoints;
            
            Skill clickedSkill;
            IEnumerable<Skill> list = null;
            GameObject btn = EventSystem.current.currentSelectedGameObject;
            
            for (int i = 0; i < skills.Count; i++)
            {
                if (btn.GetComponentInChildren<Text>().text == skills[i].Name)
                {
                    clickedSkill = skills[i];
                    list = skilltree.SkillTree.Adj(skills[i]);
                }
            }
            if (list != null)
            {
                foreach (Skill skill in list)
                {
                    for (int i = 0; i < skills.Count; i++)
                    {
                        if (skillButtons[i].GetComponentInChildren<Text>().text == skill.Name)
                        {
                            skillButtons[i].interactable = true;
                        }
                    }
                }
            }
        }
    }

    public void AddSkillPoint()
    {
        skillPoints++;
        visualTree.gameObject.SetActive(true);
        skillPointText.text = "Skill Points: " + skillPoints;
    }

}
