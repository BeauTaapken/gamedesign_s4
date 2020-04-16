using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkinColor : MonoBehaviour
{
    public Material[] skinColorMaterials;

    private SkinnedMeshRenderer skinnedMeshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        int skinColorRange = Random.Range(0, skinColorMaterials.Length);
        Material[] mats = { skinColorMaterials[skinColorRange] };
        skinnedMeshRenderer.materials = mats;
    }
}