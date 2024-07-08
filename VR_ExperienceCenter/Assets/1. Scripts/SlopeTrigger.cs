using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SlopeTrigger : MonoBehaviour
{
    [SerializeField] TimelineAsset slope;
    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playableDirector.Play();
        }
    }
}
