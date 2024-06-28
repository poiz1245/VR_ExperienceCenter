using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PortalToStage3 : MonoBehaviour
{
    [SerializeField] GameObject stencilRoom;
    [SerializeField] Collider[] childrenCollider;
    //BoxCollider collider;

    Rigidbody rigid;
    Collider collider;
    XRGrabInteractable interactable;
    PortalToStage3 portal;
    MeshRenderer meshRenderer;

    int changeVisibleLayer;
    int changeInVisibleLayer;

    bool isGrabed = false;
    public bool walkIn { get; private set; }
    public bool thirdStageStart { get; private set; }
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        portal = GetComponent<PortalToStage3>();
        collider = GetComponent<MeshCollider>();
        rigid = GetComponent<Rigidbody>();
        interactable = GetComponent<XRGrabInteractable>();
        changeVisibleLayer = LayerMask.NameToLayer("Default");
        changeInVisibleLayer = LayerMask.NameToLayer("Stencil2");
        walkIn = false;
        thirdStageStart = false;
    }

    void ChangeLayerRecursively(GameObject obj, int layer)  // ��Ż�� ���� ������Ʈ���� ���̾ �����ϴ� �޼���
    {
        obj.layer = layer;
        // ���� ������Ʈ�� �������� �ʰ�, �ڽ� ������Ʈ�� ����
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            ChangeLayerRecursively(obj.transform.GetChild(i).gameObject, layer);
        }
    }
    void DisableAllExcludedObjects()
    {
        GameObject[] allObject = GameObject.FindObjectsOfType<GameObject>();
        GameObject[] excludedObjects = System.Array.FindAll(allObject, obj => !obj.transform.IsChildOf(transform) && obj.tag != "Setting" && obj.tag != "Stage3"&& !IsChildOfPlayerObject(obj));

        foreach (GameObject obj in excludedObjects)
        {
            obj.SetActive(false);
        }
    } // �÷��̾�� �� ���� ������Ʈ �����ϰ� ��� ��Ȱ��ȭ�ϴ� �޼���
    bool IsChildOfPlayerObject(GameObject obj) //Player������Ʈ�� �� ���� ��� ������Ʈ ã�� �޼���
    {
        if (obj.CompareTag("Player"))
        {
            return true;
        }

        Transform parent = obj.transform.parent;
        while (parent != null)
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
                DisableAllExcludedObjects();
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
            thirdStageStart = true;
            RenderSettings.fog = false;
            meshRenderer.enabled = false;

            for (int i = 0; i < childrenCollider.Length; i++)
            {
                childrenCollider[i].enabled = true;
            }


            if (portal != null)
            {
                portal.enabled = false;
            }


        }
    }

}
