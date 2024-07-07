using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FallingSound : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] AudioSource audioSources;

    Rigidbody rigid;
    XRGrabInteractable interactable;
    float deltaScale;
    Vector3 startScale;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        interactable = GetComponent<XRGrabInteractable>();
        startScale = transform.localScale;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || interactable.isSelected || rigid.velocity.magnitude <= 0f)
        {
            return;
        }

        deltaScale = transform.localScale.x / startScale.x;
        if (deltaScale <= 1)
        {
            audioSources.clip = audioClips[0];
        }
        else if (deltaScale > 1 && deltaScale <= 3)
        {
            audioSources.clip = audioClips[1];
        }
        else
        {
            audioSources.clip = audioClips[2];
        }
        audioSources.Play();
    }
}
