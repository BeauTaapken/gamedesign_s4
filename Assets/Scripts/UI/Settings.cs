using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]

public class Settings : ScriptableObject
{
    private int _sensitivity;
    private bool _controller;

    public void SetSensitivity(int newSensitivity)
    {
        _sensitivity = newSensitivity;
    }

    public int GetSensitivity()
    {
        return _sensitivity;
    }

    public void SetController(bool newController)
    {
        _controller = newController;
    }

    public bool GetController()
    {
        return _controller;
    }
}
