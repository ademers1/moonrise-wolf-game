using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector]
    public CameraController Camera;
    void Awake()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    public void GetCamera()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }



}


