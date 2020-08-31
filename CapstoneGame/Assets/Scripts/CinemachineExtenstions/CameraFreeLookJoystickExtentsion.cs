using Cinemachine;
using UnityEngine;

public class CameraFreeLookJoystickExtentsion : MonoBehaviour
{
    private bool _freeLookActive;
    // Use this for initialization
    void Awake()
    {
        CinemachineCore.GetInputAxis = HandleAxisInputDelegate;

    }

    private void Update()
    {
        _freeLookActive = Input.GetMouseButton(1);
    }

    // Update is called once per frame
    float HandleAxisInputDelegate(string axisName)
    {
        Debug.Log(axisName);
        switch (axisName)
        {

            case "Right Stick X":
                return Input.GetAxis(axisName);                
            case "Right Stick Y":
                return Input.GetAxis(axisName);
            default:
                Debug.LogError("Input <" + axisName + "> not recognyzed.", this);
                break;
        }

        return 0f;
    }
}
