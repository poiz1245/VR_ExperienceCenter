using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandLight : MonoBehaviour
{
    [SerializeField] InputActionReference triggerButton;
    [SerializeField] Light handLight;

    XRGrabInteractable interactable;
    bool focusOn = false;

    void Start()
    {
        triggerButton.action.performed += HandLightFocusOn;
        handLight.type = UnityEngine.LightType.Spot;
        interactable = GetComponent<XRGrabInteractable>();
    }

    void HandLightFocusOn(InputAction.CallbackContext context)
    {
        if(interactable.isSelected)
        {
            if(!focusOn)
            {
                handLight.intensity = 200f;
                handLight.innerSpotAngle = 20f;
                handLight.spotAngle = 30f;
                focusOn = true;
            }
            else if (focusOn)
            {
                handLight.intensity = 100f;
                handLight.innerSpotAngle = 10f;
                handLight.spotAngle = 60f;
                focusOn = false;
            }
            
        }
       
    }
}
