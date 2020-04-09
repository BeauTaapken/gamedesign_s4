using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHit : MonoBehaviour
{
    [SerializeField] CapsuleCollider capsuleCollider;
    [SerializeField] HealtBar healtBar;

    bool isWaiting = false;

    float previousTime;
    float currentTime;
    public float getHitTime = 0.5f;

    public float AmountOfDamege = 5f;
        // Start is called before the first frame update
    void Start()
    {
        previousTime = Time.time;
    }
    IEnumerator Wait()
    {
        isWaiting = true;
        yield return new WaitForSeconds(getHitTime);
        isWaiting = false;
    }
    void OnTriggerStay(Collider hit)
    {
        if(hit.gameObject.tag == "Monster")
        {
            if (!isWaiting)
            {
                StartCoroutine("Wait");
                healtBar.TakeDamage(AmountOfDamege);
            }
        }
    }
}
