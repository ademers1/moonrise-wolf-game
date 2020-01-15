using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinLevel : MonoBehaviour
{
    public Camera FirstPersonCamera;
    public Camera ThirdPersonCamera;
    InventorySystem Inventory;
   
    void Start()
    {
        Inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
    }

  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

       
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Win");
            GameManager.Instance.Camera.ShowMouse();
        }
    }
}
