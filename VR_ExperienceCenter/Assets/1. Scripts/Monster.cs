using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public bool playerTag;
    [SerializeField] GameObject target;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (agent.destination != target.transform.position)
        {
            agent.SetDestination(target.transform.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerTag = true;
        }
    }
}
