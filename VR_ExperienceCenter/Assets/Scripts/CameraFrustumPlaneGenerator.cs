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
        // ī�޶� ����ü ��� ���� ��������
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        // ����ü ��� ������ �̿��Ͽ� ��� GameObject ����
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

        // ����� ���� ���Ϳ� �Ÿ� ������ �̿��Ͽ� ����� ũ��� ��ġ ����
        planeObj.transform.position = plane.normal * plane.distance;
        planeObj.transform.up = plane.normal;
        planeObj.transform.localScale = new Vector3(100f, 100f, 1f);

        return planeObj;
    }
}
