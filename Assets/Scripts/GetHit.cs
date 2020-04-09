using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHit : MonoBehaviour
{
    [SerializeField] CapsuleCollider capsuleCollider;
    [SerializeField] HealtBar healtBar;

    float previousTime;
    float currentTime;
    public float getHitTime = 0.5f;

    public float AmountOfDamege = 5f;
        // Start is called before the first frame update
    void Start()
    {
        previousTime = Time.time;
    }
    void OnTriggerStay(Collider hit)
    {
        if(hit.gameObject.tag == "Monster")
        {
            currentTime = Time.time;
            if((currentTime - previousTime) > getHitTime)
            {
                Debug.Log("Damage");
                previousTime = currentTime;
                healtBar.TakeDamage(AmountOfDamege);
            }
        }
    }
}
