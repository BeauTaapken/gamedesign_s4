using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 12f;
    public float gravity = 9.8f;

    public CharacterController controller;
    

    private float vSpeed = 0;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (controller.isGrounded)
        {
            vSpeed = 0;
            //If jumping will be added, add here with an if statement on getaxis
        }

        vSpeed -= gravity;
        move.y = vSpeed;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }
}
