using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MonsterChase : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] CloseOpenDoor door;
    [SerializeField] float moveSpeed;

    AudioSource audioSource;
    public bool playerTag { get; private set; }
    bool startChase = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (door != null && door.isOpen)
        {
            startChase = true;
        }

        if (startChase)
        {
            Rotate();
            Move();
            audioSource.Play();
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = targetRotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerTag = true;
        }
    }
}
