using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector]
    public CameraController Camera;
    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("ThirdPersonCamera").GetComponent<CameraController>();
    }

    public void Init()
    {
        Debug.Log("Initialized");
    }

}


