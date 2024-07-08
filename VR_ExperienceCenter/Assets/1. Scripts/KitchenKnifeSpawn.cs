using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenKnifeSpawn : MonoBehaviour
{
    [SerializeField] GameObject knife;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            knife.SetActive(true);
        }
    }
}
