using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{

    public float speed;
    private void Start()
    {
        speed = 0.75f;
    }
    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Input.GetAxis("Fire2"), 0f))
        {
            float horizontal = Input.GetAxis("Mouse X");
            float vertical = Input.GetAxis("Mouse Y");
        
            Quaternion rotation = transform.rotation;

            rotation.y += horizontal * Time.deltaTime * speed;
        
            transform.rotation = rotation;
        }

    }
}
