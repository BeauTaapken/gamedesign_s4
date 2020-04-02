using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAuto : MonoBehaviour
{
    [SerializeField]NavMeshSurface navMeshSurface;   
    // Start is called before the first frame update
    void Start()
    {
        navMeshSurface.BuildNavMesh();
    }


}
