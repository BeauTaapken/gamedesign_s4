using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAuto : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface[] navMeshSurfaces;   
    // Start is called before the first frame update
    void Start()
    {
        foreach (NavMeshSurface navMeshSurface in navMeshSurfaces)
        {
            navMeshSurface.BuildNavMesh();
        }
    }


}
