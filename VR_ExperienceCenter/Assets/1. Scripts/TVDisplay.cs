using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TVDisplay : MonoBehaviour
{
    [SerializeField] GameObject pointLight;
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
            pointLight.SetActive(false); 
            rigid.constraints = RigidbodyConstraints.None;
        }
    }


}
