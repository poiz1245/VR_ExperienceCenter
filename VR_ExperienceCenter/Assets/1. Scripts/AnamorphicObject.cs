using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AnamorphicObject : MonoBehaviour
{
    XRGrabInteractable interactable;
    new Collider collider;
    Rigidbody rigid;

    bool isGrab;
    void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        collider = GetComponent<Collider>();
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (interactable.isSelected && !isGrab)
        {
            isGrab = true;
            rigid.useGravity = true;
        }
        else if(!interactable.isSelected && isGrab)
        {
            isGrab = false;
            rigid.useGravity = true;
        }

        if (isGrab)
        {
            collider.isTrigger = true;
        }
        else
        {
            collider.isTrigger= false;
        }
    }
}
