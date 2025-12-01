using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseSensitivityController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetSensitivity(float sensitivity)
    {
        CinemachineInputAxisController controller = GetComponent<CinemachineInputAxisController>();

        foreach( var c in controller.Controllers)
        {
            if (c.Name == "Look Orbit X")
            {
                c.Input.Gain = sensitivity / 5f;
            }
            if (c.Name == "Look Orbit Y")
            {
                c.Input.Gain = -1f * sensitivity / 5f;
            }
        }
    }
}