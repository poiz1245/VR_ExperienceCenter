using UnityEngine;

public class CameraFrustumPlaneGenerator : MonoBehaviour
{
    public GameObject planeParent;

    private void Start()
    {
        GenerateFrustumPlanes();
    }

    private void GenerateFrustumPlanes()
    {
        // 카메라 절두체 평면 정보 가져오기
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        // 절두체 평면 정보를 이용하여 평면 GameObject 생성
        for (int i = 0; i < frustumPlanes.Length; i++)
        {
            GameObject plane = CreatePlaneGameObject(frustumPlanes[i]);
            plane.transform.SetParent(planeParent.transform);
        }
    }

    private GameObject CreatePlaneGameObject(Plane plane)
    {
        GameObject planeObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
        planeObj.name = $"Frustum Plane {plane.normal}";

        // 평면의 법선 벡터와 거리 정보를 이용하여 평면의 크기와 위치 설정
        planeObj.transform.position = plane.normal * plane.distance;
        planeObj.transform.up = plane.normal;
        planeObj.transform.localScale = new Vector3(100f, 100f, 1f);

        return planeObj;
    }
}
