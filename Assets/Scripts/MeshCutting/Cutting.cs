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
    public LivingCounterUI LivingCounterUiMonster;
    public LivingCounterUI LivingCounterUiBoss;
    public GameObject bloodParticles;

    public Audio audio;
    private float horizontal;
    private float vertical;

    private void Start()
    {
        audio = GetComponent<Audio>();
    }
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

    public void Slice(float damage)
    {
        Collider[] hits = Physics.OverlapBox(cutPlane.position, new Vector3(3.0f, 1.0f, 1.5f), cutPlane.rotation, layerMask);

        if (hits.Length <= 0)
        {
            return;
        }
        else
        {
            audio.playRandom();
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
                if (hits[i].transform.parent.name.ToLower().Contains("boss"))
                {
                    BossHealth bossHealth = hits[i].transform.parent.GetComponent<BossHealth>();
                    bossHealth.TakeDamage(damage);
                    Debug.Log("hitting boss");
                    if (bossHealth.getCurrentHealth() > bossHealth.getMinHealthToSlice())
                    {
                        return;
                    }
                }
                obj = GetBodyMesh(hits[i].gameObject);
                obj.transform.localScale = hits[i].transform.parent.localScale;
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
        
        addParticleEffect(go, 90.0f, 0.0f, 0.0f);
        addParticleEffect(go, 0.0f, 90.0f, 0.0f);
        addParticleEffect(go, 0.0f, 0.0f, 90.0f);
        addParticleEffect(go, 180.0f, 0.0f, 0.0f);
        addParticleEffect(go, 0.0f, 180.0f, 0.0f);
        addParticleEffect(go, 0.0f, 0.0f, 180.0f);

        Destroy(go, destroyTime);

        rb.AddExplosionForce(100, go.transform.position, 100);
    }

    public void addParticleEffect(GameObject parent, float rotationX, float rotationY, float rotationZ)
    {
        GameObject bloodParticlePrefab = Instantiate(bloodParticles);
        bloodParticlePrefab.transform.SetParent(parent.transform);
        bloodParticlePrefab.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        bloodParticlePrefab.transform.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
        bloodParticlePrefab.GetComponent<ParticleSystem>().Play();
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
            if (child.GetComponent<SkinnedMeshRenderer>())
            {
                return child.gameObject;
            }
        }

        return null;
    }
}