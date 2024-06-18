using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] RightController rightController;
    [SerializeField] XRGrabInteractable grabInteractable;

    bool isGrab = false;
    float initialDistance;
    Vector3 initialDirection;
    Quaternion initialRotation;

    void Update()
    {
        GetComponent<Renderer>().sortingOrder = int.MaxValue;

        if (grabInteractable.isSelected && !isGrab)
        {
            initialDistance = Vector3.Distance(transform.position, mainCamera.transform.position);
            //initialDirection = (mainCamera.transform.position - transform.position).normalized;
            initialRotation = mainCamera.transform.rotation;
            isGrab = true;
        }
        else if (grabInteractable.isSelected && isGrab)
        {
            //transform.position = mainCamera.transform.position - (initialDirection * initialDistance);
            //transform.rotation = Quaternion.LookRotation(mainCamera.transform.position - transform.position);
            transform.position = mainCamera.transform.position + (mainCamera.transform.forward * initialDistance);
            transform.rotation = initialRotation;
        }
        else if (!grabInteractable.isSelected && isGrab)
        {
            isGrab = false;
        }
    }
}
