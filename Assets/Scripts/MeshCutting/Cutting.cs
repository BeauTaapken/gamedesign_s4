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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation, 0.2f);
        RotatePlane();
    }

    public void Slice()
    {
        Collider[] hits = Physics.OverlapBox(cutPlane.position, new Vector3(5, 0.1f, 5), cutPlane.rotation, layerMask);

        if (hits.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < hits.Length; i++)
        {
            SlicedHull hull = SliceObject(hits[i].gameObject, crossMaterial);
            if (hull != null)
            {
                GameObject bottom = hull.createHull(hits[i].gameObject, crossMaterial, false);
                GameObject top = hull.createHull(hits[i].gameObject, crossMaterial, true);
                AddHullComponents(bottom);
                AddHullComponents(top);
                if (hits[i].gameObject.transform.parent)
                {
                    Destroy(hits[i].gameObject.transform.parent.gameObject);
                }
                else
                {
                    Destroy(hits[i].gameObject);
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
        if (Input.GetJoystickNames().Length > 0)
        {
            horizontal = Input.GetAxis("Mouse X");
            vertical = Input.GetAxis("Mouse Y");
            if (horizontal > deadzone || horizontal < -deadzone || vertical > deadzone || vertical < -deadzone)
            {
                cutPlane.transform.localEulerAngles = new Vector3(cutPlane.eulerAngles.x, cutPlane.eulerAngles.y, Mathf.Atan2(vertical, horizontal) * controllerRotation / Mathf.PI);
            }
        }
        //else
        //{
        //    cutPlane.eulerAngles = new Vector3(cutPlane.eulerAngles.x, cutPlane.eulerAngles.y, -Input.GetAxis("Mouse X") * 5);
        //}
    }
}
