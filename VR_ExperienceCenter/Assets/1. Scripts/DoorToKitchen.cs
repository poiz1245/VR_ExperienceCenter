using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorToKitchen : MonoBehaviour
{
    public bool path {  get; private set; }

    private void Start()
    {
        path = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            path = true;
        }
    }
}
