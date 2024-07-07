using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTrigger : MonoBehaviour
{
    [SerializeField] GameObject monster;
    [SerializeField] Vector3 move;
    [SerializeField] int duration;
    Animator animator;
    AudioSource audioSource;
    private void Start()
    {
        animator = monster.GetComponent <Animator>();
        audioSource = monster.GetComponent <AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("move", true);
            monster.transform.DOMove(move, duration).SetEase(Ease.Linear);
            audioSource.Play();
        }
    }

    IEnumerator MonsterDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(monster);
    }
}
