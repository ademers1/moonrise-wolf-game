using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Camera.ShowMouse();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
