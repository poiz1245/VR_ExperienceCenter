using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterSpawnTrigger : MonoBehaviour
{
    [SerializeField] GameObject monster;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            monster.SetActive(true);
        }
    }

   
}
