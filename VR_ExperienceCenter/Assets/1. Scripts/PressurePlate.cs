using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] GameObject barrier;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            gameObject.transform.DOLocalMove(new Vector3(0,0.001f, 0), 1).SetEase(Ease.InQuad);
            barrier.transform.DOLocalMove(new Vector3(1.04f, -2.41f, 8.8f), 3).SetEase(Ease.OutQuad);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            gameObject.transform.DOLocalMove(new Vector3(0, 0.008f, 0), 1).SetEase(Ease.InQuad);
            barrier.transform.DOLocalMove(new Vector3(1.25f, -7.8f, 8.7f), 3).SetEase(Ease.OutQuad);
        }
    }
}
