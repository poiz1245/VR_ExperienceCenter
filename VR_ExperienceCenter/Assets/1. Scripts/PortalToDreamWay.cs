using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToDreamWay : MonoBehaviour
{
    new Collider collider;
    [SerializeField] string sceneName;
    [SerializeField] OpenDoor openDoor;
    [SerializeField] Camera cinemachineCamera;

    private void Start()
    {
        collider = GetComponent<Collider>();
    }
    private void Update()
    {
        if (openDoor != null && openDoor.isOpen)
        {
            collider.isTrigger = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (cinemachineCamera != null)
            {
                cinemachineCamera.enabled = false;
            }
            Camera.main.enabled = false;
            SceneManager.LoadScene(sceneName);
        }
    }
}
