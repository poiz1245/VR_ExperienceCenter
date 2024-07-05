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
    [SerializeField] Camera timelineCamera;

    bool breachingComplete = false;
    // Start is called before the first frame update
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
            PlayerTag();
        }
    }
    void GameOver()
    {
        //게임오버
        Camera.main.enabled = false;
        timelineCamera.enabled = true;
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
        breachingComplete= true;
    }
}
