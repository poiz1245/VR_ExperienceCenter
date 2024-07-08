using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class DeadZoneMonster : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] string currentSceneName;

    [SerializeField] TimelineAsset playerDie;
    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] Camera timelineCamera;

    [SerializeField] AudioSource bgm;
    
    private void Update()
    {
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //영상 재생
            if (bgm != null)
            {
                bgm.Stop();
            }
            GameOver();
            StartCoroutine(LoadSceneWithDelay(currentSceneName, 2.5f));

        }
    }

    private IEnumerator LoadSceneWithDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    void GameOver()
    {
        //게임오버
        Camera.main.enabled = false;
        timelineCamera.enabled = true;
        playableDirector.Play();
    }
}
