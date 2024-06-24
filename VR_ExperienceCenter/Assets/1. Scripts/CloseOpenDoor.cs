using System.Collections;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class CloseOpenDoor : MonoBehaviour
{
    public Animator doorAnimatior;
    public GameObject player;
    public InputActionReference triggerButton;

    XRSimpleInteractable simpleInteractable;
    bool isOpen = false;

    private void Awake()
    {
        simpleInteractable = GetComponentInChildren<XRSimpleInteractable>();
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

        if (dist < 15)
        {
            if (!isOpen)
            {
                StartCoroutine(Opening());
            }
            else
            {
                StartCoroutine(Closing());
            }
        }
        
    }
    IEnumerator Opening()
    {
        doorAnimatior.Play("Opening 1");
        isOpen = true;
        yield return new WaitForSeconds(.5f);
    }
    IEnumerator Closing()
    {
        doorAnimatior.Play("Closing 1");
        isOpen = false;
        yield return new WaitForSeconds(.5f);
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(Closing());
    }
}
