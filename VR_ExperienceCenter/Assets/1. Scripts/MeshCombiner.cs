using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    public List<GameObject> objectsToCombine = new List<GameObject>();

   /* void Start()
    {
        // 씬 내에 있는 같은 이름의 오브젝트들을 찾아서 objectsToCombine 리스트에 추가합니다
        string objectNameToCombine = "MyObject";
        GameObject[] sameNamedObjects = GetObjectsByName(objectNameToCombine);
        objectsToCombine.AddRange(sameNamedObjects);
    }*/
    public GameObject[] GetObjectsByName(string name)
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        List<GameObject> matchingObjects = new List<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == name)
            {
                matchingObjects.Add(obj);
            }
        }
        return matchingObjects.ToArray();
    }

    public void CombineMeshes()
    {
        string objectNameToCombine = "MyObject";
        GameObject[] sameNamedObjects = GetObjectsByName(objectNameToCombine);
        objectsToCombine.AddRange(sameNamedObjects);

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
        combinedObject.AddComponent<MeshCollider>();
        combinedObject.GetComponent<MeshCollider>().convex = true;
        //combinedObject.AddComponent<Rigidbody>().isKinematic = true;
        //combinedObject.GetComponent<MeshCollider>().isTrigger = true;
        
        combinedObject.gameObject.tag = "Sliceable";

        foreach (GameObject obj in objectsToCombine)
        {
            Destroy(obj);
        }

        objectsToCombine.Clear();
    }
}