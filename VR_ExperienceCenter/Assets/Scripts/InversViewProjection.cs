using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using System;
using System.Linq;
using Unity.XR.CoreUtils;

public class InversViewProjection : MonoBehaviour
{
    bool isTriggerActive = false;
    Camera mainCamera;

    List<GameObject> insideObject = new List<GameObject>();
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Start()
    {
        FindObjectsInCameraFrustum();

    }
    private void Update()
    {
        if (!isTriggerActive && gameObject.activeSelf)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sliceable" && insideObject.Contains(other.gameObject))
        {
            isTriggerActive = true;
            Slice(other.gameObject);
        }
    }

    private void FindObjectsInCameraFrustum()
    {
        UnityEngine.Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        GameObject[] sliceableObject = GameObject.FindGameObjectsWithTag("Sliceable");

        foreach (GameObject obj in sliceableObject)
        {
            if (obj.GetComponent<MeshRenderer>() == null)
            {
                obj.AddComponent<MeshRenderer>();
            }

            Bounds bounds = obj.GetComponent<Renderer>().bounds;

            if (GeometryUtility.TestPlanesAABB(frustumPlanes, bounds))
            {
                insideObject.Add(obj);
            }

            //Destroy(obj);
        }
    }
    public void Slice(GameObject target)
    {
        SlicedHull hull = target.Slice(transform.position, transform.up);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target);
            GameObject lowerHull = hull.CreateLowerHull(target);
            SetupSlicedComponent(lowerHull);
            //SetupSlicedComponent(upperHull);
            Destroy(upperHull);
            Destroy(target.gameObject);
        }

       // Destroy(gameObject);
    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rigid = slicedObject.GetComponent<Rigidbody>();

        if (rigid == null)
        {
            rigid = slicedObject.AddComponent<Rigidbody>();
        }

        rigid.isKinematic = true;
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        slicedObject.gameObject.tag = "Sliceable";
    }
}
