using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] GameObject key;
    [SerializeField] GameObject door;
    [SerializeField] Transform photoAttach;

    XRGrabInteractable keyInteractable;
    Rigidbody keyRigidbody;
    void Start()
    {
        keyInteractable = key.GetComponent<XRGrabInteractable>();
        keyRigidbody = key.GetComponent<Rigidbody>();
    }

    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject== key)
        {
            Vector3 rotation = new Vector3(0.0f, -90.0f, 0.0f);
            int duration = 2;

            keyInteractable.enabled = false;
            keyRigidbody.isKinematic = true;

            other.transform.position = photoAttach.position;
            other.transform.rotation = photoAttach.rotation;
          


            door.transform.DORotate(rotation, duration).SetEase(Ease.InQuad);
        }
    }
}
