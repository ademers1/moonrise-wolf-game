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
            Inventory.running = false;
            SceneManager.LoadScene("Win");
            if (FirstPersonCamera.isActiveAndEnabled)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                FirstPersonCamera cam = GameObject.FindGameObjectWithTag("FirstPersonCamera").GetComponent<FirstPersonCamera>();
                cam.cam = false;
            }
            if (ThirdPersonCamera.isActiveAndEnabled)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                CameraController cam = GameObject.FindGameObjectWithTag("ThirdPersonCamera").GetComponent<CameraController>();
                cam.cam = false;
            }
        }
    }
}
