using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class FollowPlayer : MonoBehaviour
{
    Camera mainCamera;
    XRGrabInteractable grabInteractable;
    //Rigidbody rigid;
    bool isGrab = false;
    float initialDistance;
    //float rotationSpeed =100f;
    //Quaternion initialRotation;

    private void Start()
    {
        //rigid = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {
        //Vector3 directionToCamera = mainCamera.transform.forward - transform.position;
        //Quaternion deltaRotation = Quaternion.LookRotation(directionToCamera.normalized);

        //float deltaRotation = directionToCamera.x * rotationSpeed;

        GetComponent<Renderer>().sortingOrder = int.MaxValue;

        if (grabInteractable.isSelected && !isGrab)
        {
            initialDistance = Vector3.Distance(transform.position, mainCamera.transform.position);
            //initialRotation = mainCamera.transform.rotation;
            isGrab = true;
        }
        else if (grabInteractable.isSelected && isGrab)
        {
            transform.position = mainCamera.transform.position + (mainCamera.transform.forward * initialDistance);

            //rigid.MoveRotation(deltaRotation);

            /*Quaternion currentRotation = transform.rotation;
            currentRotation *= Quaternion.Euler(0f, deltaRotation, 0f);
            transform.rotation = currentRotation;*/
        }
        else if (!grabInteractable.isSelected && isGrab)
        {
            isGrab = false;
        }
    }
}
