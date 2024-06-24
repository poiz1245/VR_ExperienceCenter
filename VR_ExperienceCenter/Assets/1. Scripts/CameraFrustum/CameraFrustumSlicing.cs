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
    public CameraFrustumPlaneGenerator planeGenerator;

    public bool sliceComplete;
    private void Awake()
    {
        planeGenerator = GetComponentInParent<CameraFrustumPlaneGenerator>();
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Sliceable")
        {
            if (planeGenerator.cutKeyDown)
            {
                print("aa");
                SliceToDestroy(other.gameObject);
                sliceComplete = true;
            }
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
            Destroy(target.gameObject);
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
            Destroy(target.gameObject);

            //meshCombiner.objectsToCombine.Add(lowerHull);

            //planeGenerator.GetObjectsByName(lowerHull.name);
            //meshCombiner.objectsToCombine.Add();
        }
    }
    public void SetupSlicedComponent(GameObject slicedObject)
    {
        //Rigidbody rigid = slicedObject.GetComponent<Rigidbody>();

        //if (rigid == null)
        //{
        //    rigid = slicedObject.AddComponent<Rigidbody>();
        //}

        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        slicedObject.gameObject.tag = "Sliceable";
        //collider.isTrigger = true;
        //rigid.isKinematic = true;
    }
}
