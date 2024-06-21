using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject planeParent;
    Plane[] planes;
    private void Start()
    {
        GenerateFrustumPlanes();
    }

    private void GenerateFrustumPlanes()
    {
        // ī�޶� ���� ��������
        Camera camera = Camera.main;
        float nearClipPlane = camera.nearClipPlane;
        float farClipPlane = camera.farClipPlane;
        float fov = camera.fieldOfView;
        float aspect = camera.aspect;

        // ����ü ��� ����
        CreateFrustumPlane(camera.transform.position, camera.transform.forward, camera.transform.up, nearClipPlane, fov, aspect, 4, "Near Plane");
        CreateFrustumPlane(camera.transform.position, camera.transform.forward, camera.transform.up, farClipPlane, fov, aspect, 5, "Far Plane");
    }

    private void CreateFrustumPlane(Vector3 cameraPos, Vector3 forward, Vector3 up, float distance, float fov, float aspect, int index, string name)
    {

        GameObject planeObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
        planeObj.name = $"{name}";
        planeObj.AddComponent<InversViewProjection>();
        planeObj.AddComponent<Rigidbody>().GetComponent<Rigidbody>().isKinematic = true;
        planeObj.GetComponent<MeshCollider>().convex = true;
        planeObj.GetComponent<MeshCollider>().isTrigger = true;

        // ����� ũ��� ��ġ ���
        float halfHeight = Mathf.Tan(Mathf.Deg2Rad * fov * 0.5f) * distance;
        float halfWidth = halfHeight * aspect;
        planeObj.transform.position = cameraPos + (forward * distance);
        planeObj.transform.up = up;
        planeObj.transform.right = Vector3.Cross(forward, up);
        planeObj.transform.localScale = new Vector3(halfWidth * 2, halfHeight * 2, 1f);

        // ��� GameObject�� �θ� ������Ʈ�� �ڽ����� ����
        planeObj.transform.SetParent(planeParent.transform);
    }
}
