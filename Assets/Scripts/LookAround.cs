using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    
    public float Sensitivity = 5.0f;
    public float Smoothing = 2.0f;
    public float minimumY;
    public float maximumY;
    

    private Vector2 _mouseLook;
    private Vector2 _smoothV;

    private GameObject character;

    private float rotationY = 0F;

    void Start()
    {
        character = gameObject.transform.parent.gameObject;
    }

    void Update()
    {
        if (Mathf.Approximately(Input.GetAxis("Fire2"), 0f))
        {
            Vector2 lookAround = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            lookAround = Vector2.Scale(lookAround, new Vector2(Sensitivity * Smoothing, Sensitivity * Smoothing));
            _smoothV.x = Mathf.Lerp(_smoothV.x, lookAround.x, 1.0f / Smoothing);
            _smoothV.y = Mathf.Lerp(_smoothV.y, lookAround.y, 1.0f / Smoothing);
            _mouseLook += _smoothV;

            rotationY += Input.GetAxis("Mouse Y") * Sensitivity;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localRotation = Quaternion.AngleAxis(-rotationY, Vector3.right);

            character.transform.localRotation = Quaternion.AngleAxis(_mouseLook.x, character.transform.up);
        }
        
    }
}
