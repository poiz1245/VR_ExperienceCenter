using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class MonsterNavMesh : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] GameObject target;
    [SerializeField] TimelineAsset playerDie;
    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] AudioSource moveAudioSource;
    //[SerializeField] Camera timelineCamera;

    bool breachingComplete = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (breachingComplete)
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
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            moveAudioSource.Stop();
            PlayerTag();
        }
    }
    void GameOver()
    {
        //게임오버
        Camera.main.enabled = false;
        virtualCamera.Priority = 10;
        playableDirector.Play();
    }
    void PlayerTag()
    {
            GameOver();
            StartCoroutine(LoadSceneWithDelay("mainChase", 1.5f));
    }
    private IEnumerator LoadSceneWithDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
    public void Breaching()
    {
        moveAudioSource.Play();
        breachingComplete = true;
    }

}
