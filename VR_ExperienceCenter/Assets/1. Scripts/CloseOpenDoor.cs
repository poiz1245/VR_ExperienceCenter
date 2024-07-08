using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSimpleInteractable))]
public class CloseOpenDoor : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] InputActionReference triggerButton;
    [SerializeField] float minDistance;
    [SerializeField] Vector3 openRotation;
    [SerializeField] Vector3 closeRotation;
    [SerializeField] AudioSource audioSource;

    XRSimpleInteractable simpleInteractable;
    public bool isOpen { get; private set; } 

    private void Awake()
    {
        simpleInteractable = GetComponentInChildren<XRSimpleInteractable>();
    }
    private void Start()
    {
        isOpen = false;
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
        int duration = 2;

        if (dist < minDistance)
        {
            if (!isOpen)
            {
                gameObject.transform.DORotate(openRotation, duration).SetEase(Ease.InQuad);
                audioSource.Play();
                isOpen = true;
            }
            else
            {
                gameObject.transform.DORotate(closeRotation, duration).SetEase(Ease.InQuad);
                audioSource.Play();
                isOpen = false;
            }
        }

    }
}
