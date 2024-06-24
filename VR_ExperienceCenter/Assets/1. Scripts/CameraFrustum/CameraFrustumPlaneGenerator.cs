using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFrustumPlaneGenerator : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject planeParent;
    public GameObject insideObject;
    public CameraFrustumSlicing[] cuttingPlane;

    public List<GameObject> objectsToBeDestroyed = new List<GameObject>();
    public List<GameObject> objectsToBeCopy = new List<GameObject>();

    MeshCombiner meshCombiner;

    Plane[] planes;

    Vector3 leftCenter;
    Vector3 rightCenter;
    Vector3 bottomCenter;
    Vector3 topCenter;

    float rightNearToFar;
    float topNearToFar;
    float farPlaneWidth;

    float farWidth;
    float farHeight;

    public bool cutKeyDown = false;
    public bool copyKeyDown = false;
    private void Start()
    {
        planes = new Plane[6];
        meshCombiner = GetComponent<MeshCombiner>();
        GenerateFrustumPlanes();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            cutKeyDown = true;

            while (cutKeyDown)
            {
                bool allSliceComplete = false;

                for (int i = 0; i < cuttingPlane.Length; i++)
                {
                    if (cuttingPlane[i].sliceComplete)
                    {
                        allSliceComplete = true;
                        break;
                    }
                    else
                    {
                        allSliceComplete = false;
                        break;
                    }
                }


                if (allSliceComplete)
                {
                    FindObjectsInCameraFrustum();
                    StartCoroutine(DestroyObjects());
                    //meshCombiner.CombineMeshes();
                    break;
                }
                else
                {
                    continue;
                }
            }


        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            copyKeyDown = true;
        }
    }

    /*public void OnTriggerEnter(Collider other)
    {
        if (cutKeyDown)
        {
            for (int i = cuttingPlane.Length - 1; i >= 0; i--)
            {
                CameraFrustumSlicing copyPlane = cuttingPlane[i].GetComponent<CameraFrustumSlicing>();
                copyPlane.SliceToDestroy(other.gameObject);
            }

            FindObjectsInCameraFrustum();

            StartCoroutine(DestroyObjects());

            //meshCombiner.CombineMeshes();
            cutKeyDown = false;
        }


        if (copyKeyDown)
        {
            for (int i = 0; i < cuttingPlane.Length; i++)
            {
                CameraFrustumSlicing copyPlane = cuttingPlane[i].GetComponent<CameraFrustumSlicing>();
                copyPlane.SliceToCopy(other.gameObject);
            }

            FindObjectsInCameraFrustum();

            //copy리스트에 있는 오브젝트 저장하기
            for (int i = 0; i < objectsToBeCopy.Count; i++)
            {
                GameObject copiedObject;
                copiedObject = Instantiate(objectsToBeCopy[i], objectsToBeCopy[i].transform.position, objectsToBeCopy[i].transform.rotation);
                copiedObject.transform.SetParent(planeParent.transform);
            }
            //저장한 다음 copy리스트에 있는 오브젝트 삭제한 후 리스트 초기화
            StartCoroutine(DestroyCopyObjects());
            copyKeyDown = false;
        }
    }*/
    private IEnumerator DestroyObjects() //삭제될 오브젝트 파괴하는 코루틴
    {
        //yield return new WaitForSeconds(0.1f);

        for (int i = objectsToBeDestroyed.Count - 1; i >= 0; i--)
        {
            Destroy(objectsToBeDestroyed[i]);
        }

        objectsToBeDestroyed.Clear();

        cutKeyDown = false;
        yield return null;
    }
    private IEnumerator DestroyCopyObjects() //카피하려고 만들어둔 오브젝트 파괴하는 코루틴
    {
        yield return new WaitForSeconds(0.01f);

        for (int i = objectsToBeCopy.Count - 1; i >= 0; i--)
        {
            Destroy(objectsToBeCopy[i]);
        }

        objectsToBeCopy.Clear();
    }
    private void FindObjectsInCameraFrustum()
    {
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        GameObject[] sliceableObject = GameObject.FindGameObjectsWithTag("Sliceable");

        foreach (GameObject obj in sliceableObject)
        {
            if (obj.GetComponent<MeshRenderer>() == null)
            {
                obj.AddComponent<MeshRenderer>();
            }
            Bounds bounds = obj.GetComponent<Renderer>().bounds;
            Vector3 min = bounds.min;
            Vector3 max = bounds.max;

            bool isFullyInFrustum = true;

            foreach (Plane plane in frustumPlanes)
            {
                float minDistance = plane.GetDistanceToPoint(min);
                float maxDistance = plane.GetDistanceToPoint(max);

                if (minDistance < 0 || maxDistance < 0)
                {
                    isFullyInFrustum = false;
                    break;
                }
            }

            if (cutKeyDown && isFullyInFrustum)
            {
                objectsToBeDestroyed.Add(obj);
            }
            else if (copyKeyDown && isFullyInFrustum)
            {
                objectsToBeCopy.Add(obj);
            }
        }


    }
    private void GenerateFrustumPlanes()
    {
        GeometryUtility.CalculateFrustumPlanes(mainCamera, planes);
        CenterSetting();
        for (int i = 0; i < 4; ++i)
        {
            GameObject p = cuttingPlane[i].gameObject;
            /*GameObject p = GameObject.CreatePrimitive(PrimitiveType.Plane);
            p.transform.SetParent(planeParent.transform);
            p.gameObject.tag = "ProjectionPanel";
            p.name = $"Frustum Plane {i}";

            p.AddComponent<CameraFrustumSlicing>();
            p.AddComponent<Rigidbody>().GetComponent<Rigidbody>().isKinematic = true;
            p.GetComponent<MeshCollider>().convex = true;
            p.GetComponent<MeshCollider>().isTrigger = true;*/

            p.transform.position = -planes[i].normal * planes[i].distance;
            p.transform.rotation = Quaternion.FromToRotation(Vector3.up, planes[i].normal);

            if (i == 0)
            {
                p.transform.position = leftCenter;
                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, planes[i].normal);
                p.transform.rotation = Quaternion.Euler(90f, rotation.eulerAngles.y, rotation.eulerAngles.z);
                p.transform.localScale = new Vector3(rightNearToFar * 0.1f, 0.1f, farHeight * 0.1f);
            }
            else if (i == 1)
            {
                p.transform.position = rightCenter;
                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, planes[i].normal);
                p.transform.rotation = Quaternion.Euler(90f, rotation.eulerAngles.y, rotation.eulerAngles.z);
                p.transform.localScale = new Vector3(rightNearToFar * 0.1f, 0.1f, farHeight * 0.1f);
            }
            else if (i == 2)
            {
                p.transform.position = bottomCenter;
                p.transform.localScale = new Vector3(farWidth * 0.1f, 0.1f, topNearToFar * 0.1f);
            }
            else if (i == 3)
            {
                p.transform.position = topCenter;
                p.transform.localScale = new Vector3(farWidth * 0.1f, 0.1f, topNearToFar * 0.1f);
            }
            /*else if (i == 4)
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
            }*/
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
