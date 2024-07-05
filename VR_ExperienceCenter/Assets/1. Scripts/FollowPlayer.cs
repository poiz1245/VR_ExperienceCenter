using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class FollowPlayer : MonoBehaviour
{
    Camera mainCamera;
    XRGrabInteractable grabInteractable;
    bool isGrab = false;
    float initialDistance;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {
        GetComponent<Renderer>().sortingOrder = int.MaxValue;

        if (grabInteractable.isSelected && !isGrab)
        {
            initialDistance = Vector3.Distance(transform.position, mainCamera.transform.position);
            isGrab = true;
        }
        else if (grabInteractable.isSelected && isGrab)
        {
            transform.position = mainCamera.transform.position + (mainCamera.transform.forward * initialDistance);
        }
        else if (!grabInteractable.isSelected && isGrab)
        {
            isGrab = false;
        }
    }
}
