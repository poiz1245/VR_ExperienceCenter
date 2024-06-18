using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using Unity.VisualScripting;

public class MeshSlicer : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;


    public float cutForce = 100f;
    public Material capMaterial;

    LayerMask sliceableLayer;
    //public LayerMask slicedObjectleLayer;

    private void Start()
    {
        sliceableLayer = LayerMask.GetMask("Slicable");
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
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

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

        int layer = Mathf.RoundToInt(Mathf.Log(sliceableLayer.value, 2));
        slicedObject.layer = layer;

        rigid.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }
}
