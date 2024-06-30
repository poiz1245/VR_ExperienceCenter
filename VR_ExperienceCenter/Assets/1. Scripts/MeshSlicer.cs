using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit;

public class MeshSlicer : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;


    public float cutForce;
    public Material capMaterial;

    LayerMask sliceableLayer;
    //public LayerMask slicedObjectleLayer;

    private void Start()
    {
        sliceableLayer = LayerMask.GetMask("Sliceable");
    }
    private void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position.normalized, out RaycastHit hit, sliceableLayer);

        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }
    public void Slice(GameObject target)
    {
        print(target);
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();
        
        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        print(hull);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, capMaterial);
            GameObject lowerHull = hull.CreateLowerHull(target, capMaterial);

            SetupSlicedComponent(upperHull);
            SetupSlicedComponent(lowerHull);

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

        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;

        ForcedPerspectiveEffect forcedPerspectiveEffect = slicedObject.AddComponent<ForcedPerspectiveEffect>();
        XRGrabInteractable xrGrabInteractable = slicedObject.GetComponent<XRGrabInteractable>();

        xrGrabInteractable.trackPosition = false;
        xrGrabInteractable.retainTransformParent = false;
        xrGrabInteractable.trackScale = false;
        xrGrabInteractable.trackScale = false;
        xrGrabInteractable.throwOnDetach = false;

        int layer = Mathf.RoundToInt(Mathf.Log(sliceableLayer.value, 2));
        slicedObject.layer = layer;

        rigid.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }
}
