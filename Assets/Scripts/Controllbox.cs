using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllbox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Mouse Y");
        float vertical = Input.GetAxisRaw("Mouse X");


        if (Mathf.Approximately(Input.GetAxisRaw("Fire1"), 1))
        {
            Debug.Log(horizontal);
            Debug.Log(vertical);
            if (Mathf.Approximately(horizontal, 0.0f) && Mathf.Approximately(vertical, 0.0f))
            {
                Debug.Log("Now going forward");
            }
            else
            {
                Debug.Log("Now slashing to other side");
            }
        }
        
    }
}
