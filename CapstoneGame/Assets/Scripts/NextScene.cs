using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class NextScene : MonoBehaviour
{
    private void Start()
    {
        //GameManager.Instance.enabled = true;
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("Game_Main");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}
