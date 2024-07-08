using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChaseSoundManager : MonoBehaviour
{
    AudioSource audioSource;

    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameManager.Instance.OnCountChanged += SoundPlay;
    }

    void SoundPlay(int count)
    {
        audioSource.Stop();
        switch (count)
        {
            case 0:
                audioSource.Stop();
                return;
            case 1:
                audioSource.volume = 0.3f;
                audioSource.pitch = 0.8f;
                break;
            case 2:
                audioSource.volume = 0.6f;
                audioSource.pitch = 1f;
                break;
            case 3:
                audioSource.volume = 1f;
                audioSource.pitch = 1.2f;
                break;
            case 4:
                audioSource.Stop();
                return;
        }

        audioSource.Play();
    }
}
