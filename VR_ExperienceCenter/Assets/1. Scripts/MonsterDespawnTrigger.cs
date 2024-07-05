using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDespawnTrigger : MonoBehaviour
{
    [SerializeField] GameObject monster;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            monster.SetActive(false);
        }
    }
}
