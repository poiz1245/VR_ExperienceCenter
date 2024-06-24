using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject stencilRoom;
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
        // ���� ������Ʈ�� �������� �ʰ�, �ڽ� ������Ʈ�� ����
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

}
