using System.Collections.Generic;
using UnityEngine;

public class CameraFrustumPlaneGenerator : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject planeParent;
    public GameObject insideObject;

    List<GameObject> projections;
    Plane[] planes;

    Vector3 leftCenter;
    Vector3 rightCenter;
    Vector3 bottomCenter;
    Vector3 topCenter;

    float rightNearToFar = 0f;
    float topNearToFar = 0f;
    float farPlaneWidth = 0f;

    float farWidth = 0f;
    float farHeight = 0f;

    private void Start()
    {
        planes = new Plane[6];
        GenerateFrustumPlanes();
    }

    private void GenerateFrustumPlanes()
    {
        print(farHeight);
        GeometryUtility.CalculateFrustumPlanes(mainCamera, planes);
        print(farHeight);
        CenterSetting();
        print(farHeight);
        for (int i = 0; i < 6; ++i)
        {

            GameObject p = GameObject.CreatePrimitive(PrimitiveType.Plane);

            p.transform.SetParent(planeParent.transform);
            p.gameObject.tag = "ProjectionPanel";
            p.name = $"Frustum Plane {i}";
            p.AddComponent<InversViewProjection>();
            p.AddComponent<Rigidbody>().GetComponent<Rigidbody>().isKinematic = true;
            p.GetComponent<MeshCollider>().convex = true;
            p.GetComponent<MeshCollider>().isTrigger = true;

            p.transform.position = -planes[i].normal * planes[i].distance;
            p.transform.rotation = Quaternion.FromToRotation(Vector3.up, planes[i].normal);


            if (i == 0)
            {
                p.transform.position = leftCenter;
                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, planes[i].normal);
                p.transform.rotation = Quaternion.Euler(90f, rotation.eulerAngles.y, rotation.eulerAngles.z);
                p.transform.localScale = new Vector3(rightNearToFar * 0.1f, rightNearToFar * 0.1f, farHeight * 0.1f);
            }
            else if (i == 1)
            {
                p.transform.position = rightCenter;
                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, planes[i].normal);
                p.transform.rotation = Quaternion.Euler(90f, rotation.eulerAngles.y, rotation.eulerAngles.z);
                p.transform.localScale = new Vector3(rightNearToFar * 0.1f, rightNearToFar * 0.1f, farHeight * 0.1f);
            }
            else if (i == 2)
            {
                p.transform.position = bottomCenter;
                p.transform.localScale = new Vector3(farWidth * 0.1f, topNearToFar * 0.1f, topNearToFar * 0.1f);
            }
            else if (i == 3)
            {
                p.transform.position = topCenter;
                p.transform.localScale = new Vector3(farWidth * 0.1f, topNearToFar * 0.1f, topNearToFar * 0.1f);
            }
            else if (i == 4)
            {
                float halfHeight = Mathf.Tan(Mathf.Deg2Rad * mainCamera.fieldOfView * 0.5f) * mainCamera.nearClipPlane;
                float halfWidth = halfHeight * mainCamera.aspect;
                p.transform.position = mainCamera.transform.position + (mainCamera.transform.forward * mainCamera.nearClipPlane);
                p.transform.rotation = Quaternion.FromToRotation(Vector3.up, planes[i].normal);
                p.transform.localScale = new Vector3(halfWidth * 0.2f, 1, halfHeight * 0.2f);
            }
            else if (i == 5)
            {
                float halfHeight = Mathf.Tan(Mathf.Deg2Rad * mainCamera.fieldOfView * 0.5f) * mainCamera.farClipPlane;
                float halfWidth = halfHeight * mainCamera.aspect;
                p.transform.position = mainCamera.transform.position + (mainCamera.transform.forward * mainCamera.farClipPlane);
                p.transform.rotation = Quaternion.FromToRotation(Vector3.up, planes[i].normal);
                p.transform.localScale = new Vector3(halfWidth * 0.2f, 1, halfHeight * 0.2f);
            }
        }
    }

    private void CenterSetting()
    {
        Vector3 nearCenter = mainCamera.transform.position + (mainCamera.transform.forward * mainCamera.nearClipPlane);
        Vector3 farCenter = mainCamera.transform.position + (mainCamera.transform.forward * mainCamera.farClipPlane);

        float farHalfHeight = Mathf.Tan(Mathf.Deg2Rad * mainCamera.fieldOfView * 0.5f) * mainCamera.farClipPlane;
        float farHalfWidth = farHalfHeight * mainCamera.aspect;
        farHeight = Mathf.Floor(farHalfHeight * 2f * 10000f) / 10000f;
        farWidth = farHalfWidth * 2f;

        float nearHalfHeight = Mathf.Tan(Mathf.Deg2Rad * mainCamera.fieldOfView * 0.5f) * mainCamera.nearClipPlane;
        float nearHalfWidth = nearHalfHeight * mainCamera.aspect;

        Vector3 farTopLeft = farCenter + (mainCamera.transform.up * farHalfHeight) - (mainCamera.transform.right * farHalfWidth);
        Vector3 farBottomLeft = farCenter - (mainCamera.transform.up * farHalfHeight) - (mainCamera.transform.right * farHalfWidth);
        Vector3 farTopRight = farCenter + (mainCamera.transform.up * farHalfHeight) + (mainCamera.transform.right * farHalfWidth);
        Vector3 farBottomRight = farCenter - (mainCamera.transform.up * farHalfHeight) + (mainCamera.transform.right * farHalfWidth);

        Vector3 nearTopLeft = nearCenter + (mainCamera.transform.up * nearHalfHeight) - (mainCamera.transform.right * nearHalfWidth);
        Vector3 nearBottomLeft = nearCenter - (mainCamera.transform.up * nearHalfHeight) - (mainCamera.transform.right * nearHalfWidth);
        Vector3 nearTopRight = nearCenter + (mainCamera.transform.up * nearHalfHeight) + (mainCamera.transform.right * nearHalfWidth);
        Vector3 nearBottomRight = nearCenter - (mainCamera.transform.up * nearHalfHeight) + (mainCamera.transform.right * nearHalfWidth);

        Vector3 nearLeftCenter = (nearTopLeft + nearBottomLeft) * 0.5f;
        Vector3 nearRightCenter = (nearTopRight + nearBottomRight) * 0.5f;
        Vector3 nearBottomCenter = (nearBottomLeft + nearBottomRight) * 0.5f;
        Vector3 nearTopCenter = (nearTopLeft + nearTopRight) * 0.5f;

        Vector3 farLeftCenter = (farTopLeft + farBottomLeft) * 0.5f;
        Vector3 farRightCenter = (farTopRight + farBottomRight) * 0.5f;
        Vector3 farBottomCenter = (farBottomLeft + farBottomRight) * 0.5f;
        Vector3 farTopCenter = (farTopLeft + farTopRight) * 0.5f;

        rightCenter = (nearRightCenter + farRightCenter) * 0.5f;
        leftCenter = (nearLeftCenter + farLeftCenter) * 0.5f;
        bottomCenter = (nearBottomCenter + farBottomCenter) * 0.5f;
        topCenter = (nearTopCenter + farTopCenter) * 0.5f;

        rightNearToFar = Vector3.Distance(nearRightCenter, farRightCenter);
        topNearToFar = Vector3.Distance(nearTopCenter, farTopCenter);
        farPlaneWidth = Vector3.Distance(farTopLeft, farTopRight);
    }
}
