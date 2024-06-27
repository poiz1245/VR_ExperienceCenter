using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TVDisplay : MonoBehaviour
{
    [SerializeField] GameObject light;
    XRGrabInteractable interactor;
    Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        interactor = GetComponent<XRGrabInteractable>();
    }

    void Update()
    {
        if (interactor.isSelected)
        {
            light.SetActive(false); 
            rigid.constraints = RigidbodyConstraints.None;
        }
    }


}
