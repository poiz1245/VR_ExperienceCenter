using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FollowPlayer : MonoBehaviour
{
    Camera mainCamera;
    XRGrabInteractable grabInteractable;
    bool isGrab = false;
    float initialDistance;
    Quaternion initialRotation;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        //mainCamera = CameraCenterRayCast.instance.mainCamera;
    }
    void Update()
    {
        GetComponent<Renderer>().sortingOrder = int.MaxValue;

        if (grabInteractable.isSelected && !isGrab)
        {
            initialDistance = Vector3.Distance(transform.position, mainCamera.transform.position);
            initialRotation = mainCamera.transform.rotation;
            isGrab = true;
        }
        else if (grabInteractable.isSelected && isGrab)
        {
            transform.position = mainCamera.transform.position + (mainCamera.transform.forward * initialDistance);
            transform.rotation = initialRotation;
        }
        else if (!grabInteractable.isSelected && isGrab)
        {
            isGrab = false;
        }
    }
}
