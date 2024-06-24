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

    bool walkIn = false;
    private void Start()
    {
        changeVisibleLayer = LayerMask.NameToLayer("Default");
        changeInVisibleLayer = LayerMask.NameToLayer("Stencil6");
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
        for (int i = 0; i < childrenCollider.Length; i++)
        {
            childrenCollider[i].isTrigger = false;
        }
    }

}
