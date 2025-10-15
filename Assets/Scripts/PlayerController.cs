using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float movementSpeed;
    public Camera camera;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    private bool isGrounded;
    private Vector3 velocity;
    public float jumpPower;
    private float gravity = -9.81f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;

        //Player Vertical Movement

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        velocity.y += gravity * Time.deltaTime;

        if (isGrounded)
        {
            if (velocity.y < 0)
            {
                velocity.y = -2f;
            }
            if (keyboard.spaceKey.IsPressed())
            {
                velocity.y = jumpPower;
            }
        }

        controller.Move(velocity * Time.deltaTime);


        //Player Horizontal Movement
        

        float speedFactor = movementSpeed * Time.deltaTime;

        float forwardValue = ((keyboard.wKey.IsPressed() ? 1 : 0) - (keyboard.sKey.IsPressed() ? 1 : 0));
        float horizontalValue = ((keyboard.aKey.IsPressed() ? 1 : 0) - (keyboard.dKey.IsPressed() ? 1 : 0));

        Vector3 movementVector = Vector3.Normalize(new Vector3(
            forwardValue * camera.transform.forward.x - horizontalValue * camera.transform.forward.z,
            0,
            forwardValue * camera.transform.forward.z + horizontalValue * camera.transform.forward.x));


        if (forwardValue != 0 || horizontalValue != 0)
        {
            transform.forward = movementVector;
        }

        controller.Move(movementVector * speedFactor);
    }
}
