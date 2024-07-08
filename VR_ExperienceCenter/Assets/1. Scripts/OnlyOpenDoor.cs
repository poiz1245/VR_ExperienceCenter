using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class OnlyOpenDoor : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] InputActionReference triggerButton;
    [SerializeField] float minDistance;
    [SerializeField] Vector3 openRotation;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject activeObject;

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

            if (activeObject != null)
            {
                activeObject.SetActive(true);
            }
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
        }

    }
}
