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

        Debug.Log(hits);

        if (hits.Length <= 0)
            return;

        for (int i = 0; i < hits.Length; i++)
        {
            SlicedHull hull = SliceObject(hits[i].gameObject, crossMaterial);
            if (hull != null)
            {
                GameObject bottom = hull.CreateLowerHull(hits[i].gameObject, crossMaterial);
                GameObject top = hull.CreateUpperHull(hits[i].gameObject, crossMaterial);
                AddHullComponents(bottom);
                AddHullComponents(top);
                Destroy(hits[i].gameObject);
            }
        }
    }

    public void AddHullComponents(GameObject go)
    {
        //Magic number here, this is to get the correct number of the layer so we don't have to have an extra value to set the correct layer to the object
        go.layer = (int) Mathf.Log(layerMask.value, 2);
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        MeshCollider collider = go.AddComponent<MeshCollider>();
        collider.convex = true;

        Destroy(go, destroyTime);

        rb.AddExplosionForce(100, go.transform.position, 20);
    }

    public SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        if (obj.GetComponent<MeshFilter>() == null)
            return null;

        return obj.Slice(cutPlane.position, cutPlane.up, crossSectionMaterial);
    }

    public void RotatePlane()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            horizontal = Input.GetAxis("Mouse X");
            vertical = Input.GetAxis("Mouse Y");
            Debug.Log(horizontal + "\n" + vertical);
            if (horizontal > deadzone || horizontal < -deadzone || vertical > deadzone || vertical < -deadzone)
            {
                cutPlane.transform.localEulerAngles = new Vector3(cutPlane.eulerAngles.x, cutPlane.eulerAngles.y, Mathf.Atan2(vertical, horizontal) * controllerRotation / Mathf.PI);
            }
        }
        else
        {
            cutPlane.eulerAngles = new Vector3(cutPlane.eulerAngles.x, cutPlane.eulerAngles.y, -Input.GetAxis("Mouse X") * 5);
        }
    }
}
