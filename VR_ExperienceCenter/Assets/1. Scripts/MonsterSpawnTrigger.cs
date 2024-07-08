using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterSpawnTrigger : MonoBehaviour
{
    [SerializeField] GameObject monster;
    [SerializeField] GameObject door;
    [SerializeField] Vector3 rotation;
    [SerializeField] Vector3 move;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Breaching());
        }
    }

    IEnumerator Breaching()
    {
        monster.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        if (door != null)
        {
            door.transform.DOLocalRotate(rotation, 1).SetEase(Ease.OutQuad);
            door.transform.DOLocalMove(move, 0.5f).SetEase(Ease.OutQuad);
        }

    }
}

