using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DisplayCaseRotation : MonoBehaviour
{
    [SerializeField] RightController rightController;
    [SerializeField] float rotateSpeed;

    XRGrabInteractable grabInteractable;
    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }
    void Update()
    {
        if (grabInteractable.isSelected == true)
        {
            Vector3 offset = rightController.transform.position - rightController.rightControllerPosition;

            float deltaRotation = -offset.x * rotateSpeed;

            Quaternion currentRotation = transform.rotation;
            currentRotation *= Quaternion.Euler(0f, deltaRotation, 0f);

            transform.rotation = currentRotation;

            rightController.rightControllerPosition = rightController.transform.position;
        }
    }
}


