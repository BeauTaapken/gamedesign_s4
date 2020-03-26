﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // look at camera...
        transform.LookAt(Camera.main.transform.position, -Vector3.up);
        // then lock rotation to Y axis only...
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }
}