using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

/* Theres alot of steps prior to using this isometric camera. We need to have the camera be a child of the parent object called "CameraTarget". 
 Once that is done, we need to change the camera from Perspective to Orthographic in order to keep the look of a 2D style game with the camera.
 Changing the size in the camera will help us see how much of the terrain is available. This also needs to be adjusted to fit the game into the screen.
 There is no real script to create this isometric camera, it's all based on Unity's Main Camera. This is just a brief explaination on how to do this. */