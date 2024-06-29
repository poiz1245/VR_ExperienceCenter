using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerHaptic : MonoBehaviour
{
    [SerializeField] ScaleFromMicrophone scaleFromMicrophone;

    XRController leftController;
    private void Start()
    {
        leftController = GetComponent<XRController>();
        GameManager.Instance.OnCountChanged += OnCountChangedHandler;
    }

    void OnCountChangedHandler(int count)
    {
        if (count == 1)
        {
            OnVibration(0.3f, 3);
        }
        else if (count == 2)
        {
            OnVibration(0.6f, 3);
        }
        else if (count == 3)
        {
            OnVibration(1f, 3);
        }
    }

    void OnVibration(float amplitude, float duration)
    {
        leftController.SendHapticImpulse(amplitude, duration);

    }
}
