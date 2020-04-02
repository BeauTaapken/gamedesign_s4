using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    
    public float Sensitivity = 100.0f;
    public float minimumY;
    public float maximumY;

    private Vector2 _mouseLook;
    private Vector2 _smoothV;

    private Transform character;

    private float rotationY;
    private float rotationX;

    void Start()
    {
        character = gameObject.transform.parent.gameObject.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Mathf.Approximately(Input.GetAxis("Fire2"), 0f))
        {
            float mouseX = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;
            
            rotationY -= mouseY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localRotation = Quaternion.Euler(rotationY, 0.0f, 0.0f);

            character.transform.Rotate(Vector3.up * mouseX);
            
        }
    }
}
