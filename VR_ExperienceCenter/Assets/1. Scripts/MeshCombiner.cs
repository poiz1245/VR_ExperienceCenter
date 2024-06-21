using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    public List<GameObject> objectsToCombine;

    void Start()
    {
        CombineMeshes();
    }

    void CombineMeshes()
    {
        MeshFilter[] meshFilters = new MeshFilter[objectsToCombine.Count];
        for (int i = 0; i < objectsToCombine.Count; i++)
        {
            meshFilters[i] = objectsToCombine[i].GetComponent<MeshFilter>();
        }

        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine);

        GameObject combinedObject = new GameObject("Combined Object");
        combinedObject.AddComponent<MeshFilter>().mesh = combinedMesh;
        combinedObject.AddComponent<MeshRenderer>().material = objectsToCombine[0].GetComponent<MeshRenderer>().sharedMaterial;

        // Optionally, disable or destroy original objects
        foreach (GameObject obj in objectsToCombine)
        {
            obj.SetActive(false);
        }
    }
}