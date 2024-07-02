using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AnamorphicObject : MonoBehaviour
{
    XRGrabInteractable interactable;
    new Collider collider;

    bool isGrab;
    void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        if (interactable.isSelected && !isGrab)
        {
            isGrab = true;
        }
        else if(!interactable.isSelected && isGrab)
        {
            isGrab = false;
        }

        if(isGrab)
        {
            collider.isTrigger = true;
        }
        else
        {
            collider.isTrigger= false;
        }
    }
}
