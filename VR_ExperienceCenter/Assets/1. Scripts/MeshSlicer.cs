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

    [SerializeField] Material[] slicedObjectBaseMaterial;
    [SerializeField] Material[] slicedObjectFirstRenderMaterial;
    [SerializeField] AudioSource cutSound;

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
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, capMaterial);
            GameObject lowerHull = hull.CreateLowerHull(target, capMaterial);

            SetupSlicedComponent(upperHull, target);
            SetupSlicedComponent(lowerHull, target);

            cutSound.Play();
            Destroy(target.gameObject);
        }
    }

    public void SetupSlicedComponent(GameObject slicedObject , GameObject originalObject)
    {
        Rigidbody rigid = slicedObject.GetComponent<Rigidbody>();

        if (rigid == null)
        {
            rigid = slicedObject.AddComponent<Rigidbody>();
        }

        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;

        SlicedObject obj = slicedObject.AddComponent<SlicedObject>();
        obj.isSliced = true;
        /*ForcedPerspectiveEffect forcedPerspectiveEffect = slicedObject.AddComponent<ForcedPerspectiveEffect>();
        XRGrabInteractable xrGrabInteractable = slicedObject.GetComponent<XRGrabInteractable>();
        forcedPerspectiveEffect = slicedObject.GetComponent<ForcedPerspectiveEffect>();
        SlicedObject targetObject = originalObject.GetComponent<SlicedObject>();

        xrGrabInteractable.trackPosition = false;
        xrGrabInteractable.retainTransformParent = false;
        xrGrabInteractable.trackScale = false;
        xrGrabInteractable.trackScale = false;
        xrGrabInteractable.throwOnDetach = false;

        slicedObjectFirstRenderMaterial = targetObject.firstRenderMaterial;
        slicedObjectBaseMaterial = targetObject.baseMaterial;
        forcedPerspectiveEffect.afterGrabMaterials = slicedObjectFirstRenderMaterial;
        forcedPerspectiveEffect.unlitMaterial = slicedObjectBaseMaterial;
        forcedPerspectiveEffect.minScale = targetObject.minScale;
        forcedPerspectiveEffect.maxScale = targetObject.maxScale;*/


        //int layer = Mathf.RoundToInt(Mathf.Log(sliceableLayer.value, 2));
        //slicedObject.layer = layer;

        rigid.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }
}
