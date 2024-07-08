using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMmanager : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] OnlyOpenDoor door;

    AudioSource audioSource;

    bool soundReady = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (door.isOpen && !soundReady)
        {
            soundReady = true;
            audioSource.clip = audioClips[1];
            audioSource.Play();
        }


    }
}
