﻿using System;
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
    public LivingCounterUI LivingCounterUiMonster;
    public LivingCounterUI LivingCounterUiBoss;

    private float horizontal;
    private float vertical;
    private Vector3 center;
    private Vector3 size;

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

    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    public void Slice()
    {
        center = cutPlane.position;
        //center = new Vector3(cutPlane.position.x, cutPlane.position.y, cutPlane.position.z + 2);
        size = new Vector3(2.0f, 0.1f, 1.0f);

        Collider[] hits = Physics.OverlapBox(center, size, cutPlane.rotation, layerMask);

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
                    if (obj.transform.parent.tag == "Monster")
                    {
                        LivingCounterUiMonster.RemoveMonster();
                    }
                    else if (obj.transform.parent.tag == "Boss")
                    {
                        LivingCounterUiBoss.RemoveMonster();
                    }
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