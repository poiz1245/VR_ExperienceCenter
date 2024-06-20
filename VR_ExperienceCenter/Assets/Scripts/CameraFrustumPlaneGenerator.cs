using System.Collections.Generic;
using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine;

public class CameraFrustumPlaneGenerator : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject planeParent;
    public GameObject insideObject;

    public GameObject aroundPlane;
    public GameObject nearFarPlane;

    List<GameObject> projections;
    Plane[] planes;
    private void Start()
    {
        planes = new Plane[6];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GenerateFrustumPlanes();
        }
    }
    private void GenerateFrustumPlanes()
    {
        GeometryUtility.CalculateFrustumPlanes(mainCamera, planes);

        for (int i = 0; i < 6; ++i)
        {
            GameObject p = GameObject.CreatePrimitive(PrimitiveType.Plane);
            p.transform.SetParent(planeParent.transform);
            p.gameObject.tag = "ProjectionPanel";
            p.AddComponent<InversViewProjection>();
            p.AddComponent<Rigidbody>().GetComponent<Rigidbody>().isKinematic = true;
            p.GetComponent<MeshCollider>().convex = true;
            p.GetComponent<MeshCollider>().isTrigger = true;

            /*if (i < 4)
            {
            }
            else
            {
            }*/
            p.transform.position = -planes[i].normal * planes[i].distance;
            p.transform.rotation = Quaternion.FromToRotation(Vector3.up, planes[i].normal);
            p.transform.localScale = new Vector3(100, 100, 100);
        }
    }
}
