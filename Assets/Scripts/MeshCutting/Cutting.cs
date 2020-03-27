using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using EzySlice;
using UnityEngine;

public class Cutting : MonoBehaviour
{
    public Transform cutPlane;
    public Material crossMaterial;
    public LayerMask layerMask;
    [Range(0.0f, 4.0f)]
    public float deadzone;
    [Range(0, 360)]
    public int controllerRotation;
    public float destroyTime;

    private float horizontal;
    private float vertical;

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Input.GetAxis("Fire2"), 1))
        {
            RotatePlane();
        }
        else
        {
            resetCutPlaneXY();
        }
    }

    public void Slice()
    {
        Collider[] hits = Physics.OverlapBox(cutPlane.position, new Vector3(1.5f, 0.1f, 1.5f), cutPlane.rotation, layerMask);

        if (hits.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < hits.Length; i++)
        {
            GameObject obj;
            //Remove getbodymesh function if old way is possible(bones don't move on their own)
            if(hits[i].GetComponent<SkinnedMeshRenderer>())
            {
                obj = hits[i].gameObject;
            }
            else
            {
                obj = GetBodyMesh(hits[i].gameObject);
            }
            SlicedHull hull = SliceObject(obj, crossMaterial);
            if (hull != null)
            {
                //Remove boneLocation from creathull function if old way is possible(bones don't move on their own)
                GameObject bottom = hull.createHull(hits[i].gameObject, obj, crossMaterial, false);
                GameObject top = hull.createHull(hits[i].gameObject, obj, crossMaterial, true);
                AddHullComponents(bottom);
                AddHullComponents(top);
                if (obj.transform.parent)
                {
                    Destroy(obj.transform.parent.gameObject);
                }
                else
                {
                    Destroy(obj);
                }
            }
        }
    }

    public void AddHullComponents(GameObject go)
    {
        //(int) Mathf.Log(layerMask.value, 2) returns the correct layer value of the layermask we want to use.
        go.layer = (int)Mathf.Log(layerMask.value, 2);
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        MeshCollider meshCollider = go.AddComponent<MeshCollider>();
        meshCollider.convex = true;
        meshCollider.sharedMesh = go.GetComponent<SkinnedMeshRenderer>().sharedMesh;

        ParticleSystem ps = go.AddComponent<ParticleSystem>();

        ParticleSystem.MainModule main = ps.main;
        main.startColor = new Color(255, 0, 0);
        ParticleSystemRenderer r = ps.GetComponent<ParticleSystemRenderer>();
        r.material = crossMaterial;

        Destroy(go, destroyTime);

        rb.AddExplosionForce(100, go.transform.position, 20);
    }

    public SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        if (obj.GetComponent<MeshFilter>() == null && obj.GetComponent<SkinnedMeshRenderer>() == null)
            return null;

        return obj.Slice(cutPlane.position, cutPlane.up, crossSectionMaterial);
    }

    public void RotatePlane()
    {
        horizontal = Input.GetAxis("Mouse X");
        vertical = Input.GetAxis("Mouse Y");
        if (Input.GetJoystickNames().Length > 0 && horizontal > deadzone || Input.GetJoystickNames().Length > 0 && horizontal < -deadzone || Input.GetJoystickNames().Length > 0 && vertical > deadzone || Input.GetJoystickNames().Length > 0 && vertical < -deadzone || Input.GetJoystickNames().Length <= 0)
        {
            cutPlane.transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(vertical, horizontal) * controllerRotation / Mathf.PI);
        }
        else
        {
            resetCutPlaneXY();
        }
    }

    public void resetCutPlaneXY()
    {
        cutPlane.transform.localEulerAngles = new Vector3(0, 0, cutPlane.localEulerAngles.z);
    }

    public GameObject GetBodyMesh(GameObject obj)
    {
        obj = obj.transform.parent.gameObject;
        foreach (Transform child in obj.transform)
        {
            if (child.name == "Body_mesh")
            {
                return child.gameObject;
            }
        }

        return null;
    }
}