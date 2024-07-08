using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class CuttingDoorOpen : MonoBehaviour
{
    new Collider collider;
    public bool barrierCutted = false;

    [SerializeField] GameObject player;
    [SerializeField] InputActionReference triggerButton;
    [SerializeField] float minDistance;
    [SerializeField] Vector3 openRotation;
    [SerializeField] Vector3 closeRotation;
    [SerializeField] GameObject portal;

    AudioSource audioSource;
    XRSimpleInteractable simpleInteractable;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        triggerButton.action.performed += TriggerButtonClicked;
        simpleInteractable = GetComponentInChildren<XRSimpleInteractable>();
        collider = GetComponent<Collider>();

        collider.isTrigger = true;
    }

    void Update()
    {
        if (barrierCutted)
        {
            collider.isTrigger = false;
        }
    }
    void TriggerButtonClicked(InputAction.CallbackContext context)
    {
        if (simpleInteractable.isHovered)
        {
            DoorInteraction();
            audioSource.Play();
        }
    }
    public void DoorInteraction()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);

        int duration = 2;

        if (dist < minDistance)
        {
            if (barrierCutted)
            {
                gameObject.transform.DORotate(openRotation, duration).SetEase(Ease.InQuad);
                portal.SetActive(true);
            }
        }

    }
}
