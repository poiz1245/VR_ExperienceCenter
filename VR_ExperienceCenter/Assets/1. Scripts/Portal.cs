using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] GameObject stencilRoom;
    [SerializeField] Collider[] childrenCollider;
    //BoxCollider collider;

    int changeVisibleLayer;
    int changeInVisibleLayer;

    public bool walkIn { get; private set; }
    private void Start()
    {
        changeVisibleLayer = LayerMask.NameToLayer("Default");
        changeInVisibleLayer = LayerMask.NameToLayer("Stencil5");
        walkIn = false;
    }
    void ChangeLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        // 현재 오브젝트는 변경하지 않고, 자식 오브젝트만 변경
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            ChangeLayerRecursively(obj.transform.GetChild(i).gameObject, layer);
        }
    }
    void DisableAllExcludedObjects()
    {
        GameObject[] allObject = GameObject.FindObjectsOfType<GameObject>();
        GameObject[] excludedObjects = System.Array.FindAll(allObject, obj => !obj.transform.IsChildOf(transform) && obj.tag != "Setting" && !IsChildOfPlayerObject(obj)); //자식이 아니고 플레이어 태그 아니면 true

        foreach (GameObject obj in excludedObjects)
        {
            obj.SetActive(false);
        }
    }
    bool IsChildOfPlayerObject(GameObject obj)
    {
        if (obj.CompareTag("Player"))
        {
            return true;
        }

        Transform parent = obj.transform.parent;
        while(parent != null)
        {
            if (parent.gameObject.CompareTag("Player"))
            {
                return true;
            }
            parent = parent.parent;
        }
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!walkIn)
            {
                walkIn = true;
                ChangeLayerRecursively(stencilRoom, changeVisibleLayer);
            }
            else
            {
                walkIn = false;
                ChangeLayerRecursively(stencilRoom, changeInVisibleLayer);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && walkIn)
        {
            for (int i = 0; i < childrenCollider.Length; i++)
            {
                childrenCollider[i].isTrigger = false;
            }

            DisableAllExcludedObjects();
        }
    }

}
