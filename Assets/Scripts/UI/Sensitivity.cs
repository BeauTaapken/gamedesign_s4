using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]

public class Sensitivity : ScriptableObject
{
    public int _sensitivity;

    // Start is called before the first frame update
    void Start()
    {
        _sensitivity = 100;
    }

    public void SetSensitivity(int newSensitivity)
    {
        _sensitivity = newSensitivity;
    }

    public int GetSensitivity()
    {
        return _sensitivity;
    }
}
