using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    
    public float Sensitivity = 5.0f;
    public float minimumY;
    public float maximumY;

    private Vector2 _mouseLook;
    private Vector2 _smoothV;

    private GameObject character;

    private float rotationY;
    private float rotationX;

    void Start()
    {
        character = gameObject.transform.parent.gameObject;
    }

    void Update()
    {
        if (Mathf.Approximately(Input.GetAxis("Fire2"), 0f))
        {
            Vector2 lookAround = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            rotationY += lookAround.y * Sensitivity;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            rotationX += lookAround.x * Sensitivity;

            transform.localRotation = Quaternion.AngleAxis(-rotationY, Vector3.right);

            character.transform.localRotation = Quaternion.AngleAxis(rotationX, character.transform.up);
        }
        
    }
}
