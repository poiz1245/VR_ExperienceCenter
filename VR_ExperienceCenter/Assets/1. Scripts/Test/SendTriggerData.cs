using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendTriggerData : MonoBehaviour
{
    PortalToSecondStage door;

    bool isTrigger = false;
    private void Start()
    {
        door = GetComponentInParent<PortalToSecondStage>();
    }
    private void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isTrigger)
            {
                isTrigger = true;
            }
            else
            {
                isTrigger = false;
            }
        }
    }
}
