using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;

    public Transform groundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask groundMask;


    public float speed = 12f;
    public float gravity = -9.8f;

    bool IsGrounded;

    public Vector3 velocity;
    void Update()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, GroundDistance, groundMask);

        

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //velocity.y += gravity * Time.deltaTime;

        if (IsGrounded)
        {
            velocity.y = 0f;
            Debug.Log("isGrounded");
        }
        else if (!IsGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        controller.Move(velocity);
    }
}
