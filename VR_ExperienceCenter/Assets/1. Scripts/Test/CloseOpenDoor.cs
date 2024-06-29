using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class CloseOpenDoor : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] InputActionReference triggerButton;

    //XRGrabInteractable interactable;
    XRSimpleInteractable simpleInteractable;
    bool isOpen = false;

    private void Awake()
    {
        simpleInteractable = GetComponentInChildren<XRSimpleInteractable>();
        //interactable = GetComponent<XRGrabInteractable>();
    }
    private void Start()
    {
        triggerButton.action.performed += TriggerButtonClicked;
    }

    void TriggerButtonClicked(InputAction.CallbackContext context)
    {
        if (simpleInteractable.isHovered)
        {
            DoorInteraction();
        }
    }
    public void DoorInteraction()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        Vector3 openRotation = new Vector3(0.0f, 30.0f, 0.0f);
        Vector3 closeRotation = new Vector3(0.0f, 180.0f, 0.0f);
        int duration = 2;

        if (dist < 3)
        {
            if (!isOpen)
            {
                gameObject.transform.DORotate(openRotation, duration).SetEase(Ease.InQuad);
                isOpen = true;
            }
            else
            {
                gameObject.transform.DORotate(closeRotation, duration).SetEase(Ease.InQuad);
                isOpen = false;
            }
        }

    }
}
