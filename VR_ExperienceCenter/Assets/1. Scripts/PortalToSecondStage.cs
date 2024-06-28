using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PortalToSecondStage : MonoBehaviour
{
    [SerializeField] GameObject firstStageMap;
    [SerializeField] GameObject secondStageMap;
    [SerializeField] Collider[] secondStageCollider;
    [SerializeField] Transform teleportPoint;
    [SerializeField] GameObject player;

    XRGrabInteractable interactable;
    PortalToSecondStage portal;
    Rigidbody rigid;

    bool isTrigger = false;
    public bool secondStageStart;
    private void Start()
    {
        rigid = GetComponentInParent<Rigidbody>();
        interactable= GetComponentInParent<XRGrabInteractable>();
        portal = GetComponent<PortalToSecondStage>();
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            secondStageStart = true;
            other.transform.position = teleportPoint.position;
            other.transform.rotation = teleportPoint.rotation;

            other.GetComponent<CharacterController>().center = teleportPoint.InverseTransformPoint(other.transform.position);
            other.GetComponent<CharacterController>().height = teleportPoint.InverseTransformPoint(other.transform.position + Vector3.up * other.GetComponent<CharacterController>().height).y;

            isTrigger = true;
            firstStageMap.SetActive(false);
            ChangeLayerRecursively(secondStageMap, 0);
            rigid.constraints = RigidbodyConstraints.FreezeAll;

            for (int i = 0; i < secondStageCollider.Length; i++)
            {
                secondStageCollider[i].enabled = true;
            }
            portal.enabled = false;
            interactable.enabled = false;
        }
    }
}
