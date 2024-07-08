using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static GameManager;

public class Hide : MonoBehaviour
{
    [SerializeField] ScaleFromMicrophone scaleFromMicrophone;

    public float hideTime;
    bool stayHide = false;


    void Start()
    {
        scaleFromMicrophone.OnSoundVolumeChanged += SoundVolumeWarning;
    }


    void SoundVolumeWarning(float loudness)
    {
        if (loudness >= GameManager.Instance.maxSoundVolume)
        {
            hideTime = 0;
        }
    }


    void Update()
    {
        if (stayHide)
        { 
            hideTime += Time.deltaTime;

            if (hideTime >= 3)
            {
                GameManager.Instance.chaseStart = false;
                GameManager.Instance.count = 0;
            }
        }
        else
        {
            hideTime = 0;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bush"))
        {
            stayHide = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bush"))
        {
            stayHide = false;
        }
    }
}
