using UnityEngine;

public class PortalTest : MonoBehaviour
{
    [SerializeField] GameObject firstStageMap;
    [SerializeField] GameObject secondStageMap;
    [SerializeField] Collider[] secondStageCollider;
    [SerializeField] Transform teleportPoint;
    [SerializeField] GameObject player;

    bool isTrigger = false;
    private void Update()
    {
        /*if (isTrigger)
        {
            player.transform.position = teleportPoint.position;
            isTrigger = false;
        }*/
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
        // 플레이어의 CharacterController가 문 Trigger에 들어오면
        if (other.GetComponent<CharacterController>() != null)
        {
            // 플레이어의 실제 위치를 목적지 Transform으로 설정
            other.transform.position = teleportPoint.position;
            other.transform.rotation = teleportPoint.rotation;

            // CharacterController의 center와 height 속성 업데이트
            other.GetComponent<CharacterController>().center = teleportPoint.InverseTransformPoint(other.transform.position);
            other.GetComponent<CharacterController>().height = teleportPoint.InverseTransformPoint(other.transform.position + Vector3.up * other.GetComponent<CharacterController>().height).y;

            isTrigger = true;
            firstStageMap.SetActive(false);
            ChangeLayerRecursively(secondStageMap, 0);
            for (int i = 0; i < secondStageCollider.Length; i++)
            {
                secondStageCollider[i].enabled = true;
            }
        }
    }
}
