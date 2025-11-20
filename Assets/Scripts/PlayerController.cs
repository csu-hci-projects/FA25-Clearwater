using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Animator playerAnimator;
    public float movementSpeed;
    public Camera camera;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    private bool isGrounded;
    private Vector3 velocity;
    public float jumpPower;
    private float gravity = -9.81f;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Cursor Lock State, this will need to change if the cursor is being used in a title screen or other UI element
        Cursor.lockState = CursorLockMode.Locked;
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
                playerAnimator.SetBool("Jump", true);
            }
            playerAnimator.SetBool("OnGround", true);
        }
        else
        {
            playerAnimator.SetBool("OnGround", false);
        }

        controller.Move(velocity * Time.deltaTime);


        //Player Horizontal Movement
        

        float speedFactor = movementSpeed * Time.deltaTime;

        float forwardValue = ((keyboard.wKey.IsPressed() ? 1 : 0) - (keyboard.sKey.IsPressed() ? 1 : 0));
        float horizontalValue = ((keyboard.dKey.IsPressed() ? 1 : 0) - (keyboard.aKey.IsPressed() ? 1 : 0));

        /* Vector3 movementVector = Vector3.Normalize(new Vector3(
            forwardValue * camera.transform.forward.x - horizontalValue * camera.transform.forward.z,
            0,
            forwardValue * camera.transform.forward.z + horizontalValue * camera.transform.forward.x)); */

        Vector3 direction = new Vector3(horizontalValue, 0f, forwardValue).normalized;


        if (forwardValue != 0 || horizontalValue != 0)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //transform.forward = movementVector;
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speedFactor);

            playerAnimator.SetBool("IsWalking", true);
        }
        else
        {
            playerAnimator.SetBool("IsWalking", false);
        }

        
    }
}
