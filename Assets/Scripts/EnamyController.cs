using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnamyController : MonoBehaviour
{
    public Animator Animator;
    public NavMeshAgent agent;

    void Update()
    {
        //agent.SetDestination(Player.transform.position);
        agent.SetDestination(Camera.main.transform.position);

        float distance = agent.remainingDistance;
        if (distance <= 3.0f)
        {
            Animator.SetBool("attacking", true);
        }
        else if (distance > 3.0f)
        {
            Animator.SetBool("attacking", false);
        }
    }
}
