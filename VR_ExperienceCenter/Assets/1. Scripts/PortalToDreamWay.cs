using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToDreamWay : MonoBehaviour
{
    new Collider collider;
    [SerializeField] string sceneName;
    [SerializeField] OpenDoor openDoor;

    private void Start()
    {
        collider = GetComponent<Collider>();
    }
    private void Update()
    {
        if (openDoor.isOpen)
        {
            collider.isTrigger = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Camera.main.enabled = false;
            SceneManager.LoadScene(sceneName);
        }
    }
}
