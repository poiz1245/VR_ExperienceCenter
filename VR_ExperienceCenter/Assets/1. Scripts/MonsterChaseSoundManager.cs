using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChaseSoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    AudioSource audioSource;

    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameManager.Instance.OnCountChanged += SoundPlay;
    }

    void SoundPlay(int count)
    {
        switch (count)
        {
            case 0:
                audioSource.Stop();
                return;
                break;
            case 1:
                audioSource.clip = audioClips[0];
                break;
            case 2:
                audioSource.clip = audioClips[1];
                break;
            case 3:
                audioSource.clip = audioClips[2];
                break;
        }

        audioSource.Play();
    }
}
