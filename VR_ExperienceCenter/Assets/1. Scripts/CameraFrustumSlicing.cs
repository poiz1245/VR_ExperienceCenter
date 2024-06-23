using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using System;
using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine.SocialPlatforms.Impl;

public class CameraFrustumSlicing : MonoBehaviour
{
    Camera mainCamera;
    CameraFrustumPlaneGenerator planeGenerator;
    MeshCombiner meshCombiner;
    private void Awake()
    {
        mainCamera = Camera.main;
        planeGenerator = GetComponentInParent<CameraFrustumPlaneGenerator>();
        meshCombiner = GetComponentInParent<MeshCombiner>();
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Sliceable")
        {
            CameraFrustumPlaneGenerator generator = GetComponentInParent<CameraFrustumPlaneGenerator>();
            generator.OnTriggerEnter(other);
            //Slice(other.gameObject);
        }
    }

    public void SliceToCopy(GameObject target)
    {
        SlicedHull hull = target.Slice(transform.position, transform.up);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target);
            SetupSlicedComponent(upperHull);
            upperHull.name = "inside" + target.name;

            planeGenerator.objectsToBeCopy.Add(upperHull);
        }
    }
    public void SliceToDestroy(GameObject target)
    {
        SlicedHull hull = target.Slice(transform.position, transform.up);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target);
            GameObject lowerHull = hull.CreateLowerHull(target);
            SetupSlicedComponent(lowerHull);
            SetupSlicedComponent(upperHull);

            upperHull.name = "inside" + target.name;
            lowerHull.name = "outside" + target.name;

            planeGenerator.objectsToBeDestroyed.Add(upperHull);
            meshCombiner.objectsToCombine.Add(lowerHull);
            Destroy(target.gameObject);
        }
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
        //collider.isTrigger = true;
        slicedObject.gameObject.tag = "Sliceable";
    }
}
