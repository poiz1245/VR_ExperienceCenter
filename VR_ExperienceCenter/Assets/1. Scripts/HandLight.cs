using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

public class HandLight : MonoBehaviour
{
    [SerializeField] InputActionReference triggerButton;
    [SerializeField] Light handLight;
    bool focusOn = false;

    void Start()
    {
        triggerButton.action.performed += HandLightFocusOn;
        handLight.type = UnityEngine.LightType.Spot;
    }

    void HandLightFocusOn(InputAction.CallbackContext context)
    {
        if (!focusOn)
        {
            handLight.intensity = 100f;
            handLight.innerSpotAngle = 10f;
            handLight.spotAngle = 60f;
            focusOn = true;
        }
        else
        {
            handLight.intensity = 200f;
            handLight.innerSpotAngle = 20f;
            handLight.spotAngle = 30f;
            focusOn = false;
        }
    }
}
