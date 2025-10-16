using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    public float cameraRotationSpeed = 2f;
    private float cameraRevolutionDegrees = 180f;
    private float cameraVerticalDegrees = 0f;
    public float cameraDistance = 10f;
    public float MaxHorizontalOffset = 0f;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mouse = Mouse.current;
        Quaternion camDirection = transform.rotation;
        Vector2 mouseMovementDelta = mouse.delta.ReadValue();
        float horizontalRotationSpeed = mouseMovementDelta.x * cameraRotationSpeed / 10;
        float verticalRotationSpeed = mouseMovementDelta.y * cameraRotationSpeed / 10;

        cameraRevolutionDegrees = (cameraRevolutionDegrees + horizontalRotationSpeed) % 360;
        cameraVerticalDegrees = (cameraVerticalDegrees + verticalRotationSpeed) % 360;
        cameraVerticalDegrees = Mathf.Clamp(cameraVerticalDegrees, -89f, 89f);

        Vector3 targetPoint = player.transform.position + new Vector3
            (
                Mathf.Sin(cameraRevolutionDegrees * 2 * Mathf.PI / 360) * Mathf.Sin(cameraVerticalDegrees * 2 * Mathf.PI / 360) * MaxHorizontalOffset,
                1f,
                Mathf.Cos(cameraRevolutionDegrees * 2 * Mathf.PI / 360) * Mathf.Sin(cameraVerticalDegrees * 2 * Mathf.PI / 360) * MaxHorizontalOffset
            );

        transform.position = new Vector3
            (
                targetPoint.x + Mathf.Sin(cameraRevolutionDegrees * 2 * Mathf.PI / 360) * Mathf.Cos(cameraVerticalDegrees * 2 * Mathf.PI / 360) * cameraDistance,
                targetPoint.y - Mathf.Sin(cameraVerticalDegrees * 2 * Mathf.PI / 360) * cameraDistance,
                targetPoint.z + Mathf.Cos(cameraRevolutionDegrees * 2 * Mathf.PI / 360) * Mathf.Cos(cameraVerticalDegrees * 2 * Mathf.PI / 360) * cameraDistance 
            );


        
        transform.LookAt(targetPoint);
    }
}
