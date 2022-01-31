using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public bool isCrouching = false;
    public float crouchingMulti;
    public float reduceHeight;
    float originalHeight;

    float originalY;

    Vector3 velocity;

    bool isGrounded;

    void Start()
    {
        originalHeight = controller.height;
        originalY = controller.transform.position.y;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftControl))
            isCrouching = true;
        else
            isCrouching = false;

        if(isCrouching == true)
        {
            controller.height = reduceHeight;
            move *= crouchingMulti;
        }
        else
        {
            controller.height = originalHeight;
            if(controller.transform.position.y < originalY)
                velocity.y = 2f;
        }
    }
}
