using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    public Settings Settings;
    public float minimumY;
    public float maximumY;

    private Vector2 _mouseLook;
    private Vector2 _smoothV;
    private int SensitivityValue;

    private Transform character;

    private float rotationY;
    private float rotationX;

    void Start()
    {
        character = gameObject.transform.parent.gameObject.transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SensitivityValue = Settings.GetSensitivity();
    }

    void Update()
    {
        if (Mathf.Approximately(Input.GetAxis("Fire2"), 0f))
        {
            float mouseX = Input.GetAxis("Mouse X") * SensitivityValue * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * SensitivityValue * Time.deltaTime;
            
            rotationY -= mouseY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localRotation = Quaternion.Euler(rotationY, 0.0f, 0.0f);

            character.transform.Rotate(Vector3.up * mouseX);
            
        }
    }
}
