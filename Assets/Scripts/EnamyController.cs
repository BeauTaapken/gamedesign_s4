using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnamyController : MonoBehaviour
{
    public GameObject Player;
    public NavMeshAgent agent;


    void Update()
    {
        //agent.SetDestination(Player.transform.position);
        agent.SetDestination(Camera.main.transform.position);
    }
}
