using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Portal : MonoBehaviour
{
    [SerializeField] GameObject stencilRoom;
    [SerializeField] Collider[] childrenCollider;
    [SerializeField] Transform teleportPoint;
    //BoxCollider collider;

    Rigidbody rigid;
    //Collider collider;
    XRGrabInteractable interactable;
    Portal portal;

    int changeVisibleLayer;
    int changeInVisibleLayer;

    //bool isGrabed = false;
    public bool walkIn { get; private set; }
    public bool secondStageStart { get; private set; }
    private void Start()
    {
        //collider = GetComponent<MeshCollider>();
        rigid = GetComponent<Rigidbody>();
        portal = GetComponent<Portal>();
        interactable = GetComponent<XRGrabInteractable>();
        changeVisibleLayer = LayerMask.NameToLayer("Default");
        changeInVisibleLayer = LayerMask.NameToLayer("Stencil5");
        walkIn = false;
        secondStageStart = false;
    }

    private void Update()
    {
        if (interactable.isSelected)
        {
            rigid.constraints &= ~RigidbodyConstraints.FreezePositionY;
        }
    }
    void ChangeLayerRecursively(GameObject obj, int layer)  // 포탈의 하위 오브젝트들의 레이어를 변경하는 메서드
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
        GameObject[] excludedObjects = System.Array.FindAll(allObject, obj => !obj.transform.IsChildOf(transform) && !IsChildOfPlayerObject(obj) && obj.tag != "Stage2" && obj.tag != "stage3" && obj.tag != "Setting"); //자식이 아니고 플레이어 태그 아니면 true

        foreach (GameObject obj in excludedObjects)
        {
            obj.SetActive(false);
        }
    } // 플레이어와 그 하위 오브젝트 제외하고 모두 비활성화하는 메서드
    bool IsChildOfPlayerObject(GameObject obj) //Player오브젝트와 그 하위 모든 오브젝트 찾는 메서드
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
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collider.isTrigger = true;
        }

        
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            // 플레이어의 실제 위치를 목적지 Transform으로 설정
            other.transform.position = other.transform.position + new Vector3(0, 0, 1.5f);
            other.transform.rotation = teleportPoint.rotation;

            // CharacterController의 center와 height 속성 업데이트
            other.GetComponent<CharacterController>().center = teleportPoint.InverseTransformPoint(other.transform.position);
            other.GetComponent<CharacterController>().height = teleportPoint.InverseTransformPoint(other.transform.position + Vector3.up * other.GetComponent<CharacterController>().height).y;


            ChangeLayerRecursively(stencilRoom, changeVisibleLayer);
            DisableAllExcludedObjects();

            secondStageStart = true;
            rigid.constraints = RigidbodyConstraints.FreezeAll;
            for (int i = 0; i < childrenCollider.Length; i++)
            {
                childrenCollider[i].enabled = true;
            }

            interactable.enabled = false;
            portal.enabled = false;


            /*   print("충돌");
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
               }*/
        }
    }
    /*private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && walkIn)
        {
            secondStageStart = true;
            rigid.constraints = RigidbodyConstraints.FreezeAll;
            for (int i = 0; i < childrenCollider.Length; i++)
            {
                childrenCollider[i].enabled = true;
            }

            interactable.enabled = false;
            portal.enabled = false;


        }
    }*/

}
