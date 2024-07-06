using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class MoveSound : MonoBehaviour
{
    [SerializeField] InputActionReference thumbStick;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        thumbStick.action.started += Move;
        thumbStick.action.canceled += Stop;
    }

    void Move(InputAction.CallbackContext context)
    {
        audioSource.Play();
    }
    void Stop(InputAction.CallbackContext context)
    {
        audioSource.Stop();
    }
}
