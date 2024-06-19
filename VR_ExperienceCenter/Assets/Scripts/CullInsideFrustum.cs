using UnityEngine;
using System.Collections.Generic;

public class CullInsideFrustum : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3[] frustumCorners;

    private void Start()
    {
        mainCamera = Camera.main;
        UpdateFrustumCorners();
    }

    private void UpdateFrustumCorners()
    {
        frustumCorners = new Vector3[4];
        mainCamera.CalculateFrustumCorners(new Rect(0, 0, 1, 1), mainCamera.nearClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumCorners);

        for (int i = 0; i < frustumCorners.Length; i++)
        {
            frustumCorners[i] = transform.InverseTransformPoint(frustumCorners[i]);
            print(frustumCorners[i]);
        }
    }

    private void LateUpdate()
    {
        CullMeshInsideFrustum();
    }

    private void CullMeshInsideFrustum()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null || meshFilter.sharedMesh == null)
            return;

        Vector3[] vertices = meshFilter.sharedMesh.vertices;
        int[] triangles = meshFilter.sharedMesh.triangles;

        List<Vector3> newVertices = new List<Vector3>();
        List<int> newTriangles = new List<int>();

        for (int i = 0; i < vertices.Length; i++)
        {
            if (IsVertexOutsideFrustum(vertices[i]))
            {
                newVertices.Add(vertices[i]);
            }
        }

        for (int i = 0; i < triangles.Length; i += 3)
        {
            if (IsVertexOutsideFrustum(vertices[triangles[i]]) ||
                IsVertexOutsideFrustum(vertices[triangles[i + 1]]) ||
                IsVertexOutsideFrustum(vertices[triangles[i + 2]]))
            {
                newTriangles.Add(triangles[i]);
                newTriangles.Add(triangles[i + 1]);
                newTriangles.Add(triangles[i + 2]);
            }
        }

        Mesh newMesh = new Mesh();
        newMesh.vertices = newVertices.ToArray();
        newMesh.triangles = newTriangles.ToArray();
        meshFilter.mesh = newMesh;
    }

    private bool IsVertexOutsideFrustum(Vector3 vertex)
    {
        for (int i = 0; i < frustumCorners.Length; i++)
        {
            if (vertex.x < frustumCorners[i].x || vertex.y < frustumCorners[i].y || vertex.z < frustumCorners[i].z)
            {
                return true;
            }
        }
        return false;
    }
}
