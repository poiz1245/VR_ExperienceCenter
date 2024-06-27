using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendTriggerData : MonoBehaviour
{
    PortalTest door;

    bool isTrigger = false;
    private void Start()
    {
        door = GetComponentInParent<PortalTest>();
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
