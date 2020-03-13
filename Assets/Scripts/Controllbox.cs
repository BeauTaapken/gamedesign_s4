using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllbox : MonoBehaviour
{
    public GameObject withCuttingScript;

    private Cutting cutting;

    // Start is called before the first frame update
    void Start()
    {
        cutting = withCuttingScript.GetComponent<Cutting>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Input.GetAxis("Fire1"), 1))
        {
            cutting.Slice();
        }
        
    }
}
