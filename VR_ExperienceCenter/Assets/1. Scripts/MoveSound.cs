using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(AudioSource))]
public class MoveSound : MonoBehaviour
{
    [SerializeField] InputActionReference thumbStick;
    [SerializeField] AudioClip[] audioClips;

    AudioSource audioSource;
    CharacterController characterController;

    bool isGrounded = false;
    bool wasGrounded = false;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        thumbStick.action.started += Move;
        thumbStick.action.canceled += Stop;
    }

    private void Update()
    {
        wasGrounded = isGrounded;
        isGrounded = characterController.isGrounded;

        print("이전프레임"+wasGrounded);
        print("현재프레임"+isGrounded);
        if (!wasGrounded && isGrounded)
        {
            PlayLandingSound();
        }
    }
    void PlayLandingSound()
    {
        audioSource.clip = audioClips[1];
        audioSource.loop = false;
        audioSource.pitch = 1.0f;
        audioSource.Play();
    }
    void Move(InputAction.CallbackContext context)
    {
        if (!characterController.isGrounded)
        {
            return;
        }

        audioSource.clip = audioClips[0];
        audioSource.loop = true;
        audioSource.pitch = 2.4f;
        audioSource.Play();
    }
    void Stop(InputAction.CallbackContext context)
    {
        audioSource.Stop();
    }
}
