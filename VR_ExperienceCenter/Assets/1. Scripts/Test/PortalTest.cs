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

    void ChangeLayerRecursively(GameObject obj, int layer)  // ��Ż�� ���� ������Ʈ���� ���̾ �����ϴ� �޼���
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
        // �÷��̾��� CharacterController�� �� Trigger�� ������
        if (other.GetComponent<CharacterController>() != null)
        {
            // �÷��̾��� ���� ��ġ�� ������ Transform���� ����
            other.transform.position = teleportPoint.position;
            other.transform.rotation = teleportPoint.rotation;

            // CharacterController�� center�� height �Ӽ� ������Ʈ
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
