using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteLowObject : MonoBehaviour
{
    public LivingCounterUI LivingCounterUi;

    public float DestroyHeight = -5;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < DestroyHeight)
        {
            Destroy(gameObject);
            LivingCounterUi.RemoveMonster();
        }
    }
}
