using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScaredTrapTrigger : MonoBehaviour
{
    [SerializeField] GameObject trapObject;
    [SerializeField] Vector3 move;
    [SerializeField] Vector3 rotation;
    [SerializeField] float duration;
    [SerializeField] DOTweenAnimation light;

    [SerializeField] AudioSource audioSource;
    [SerializeField] float delay;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }

            if (trapObject != null)
            {
                trapObject.transform.DOMove(move, duration).SetEase(Ease.Linear);
                trapObject.transform.DORotate(rotation, duration).SetEase(Ease.Linear);

                if (light != null)
                {
                    light.DOPlay();  
                }
            }

            StartCoroutine(SoundDestroy());

        }
    }
    IEnumerator SoundDestroy()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);

    }
}
