using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIrtualRoomTrigger : MonoBehaviour
{
    [SerializeField] GameObject virtualRoom;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            virtualRoom.SetActive(true);
        }
    }
}
