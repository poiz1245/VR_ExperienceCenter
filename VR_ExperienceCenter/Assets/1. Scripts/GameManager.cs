using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] string currentSceneName;

    public float maxSoundVolume;
    [SerializeField] ScaleFromMicrophone scaleFromMicrophone;

    [SerializeField] TimelineAsset playerDie;
    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] Camera timelineCamera;

    [SerializeField] MonsterChase monster;

    Camera mainCamera;

    public int count;

    public delegate void CountChanged(int count);
    public event CountChanged OnCountChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }
    private void Start()
    {
        playableDirector.playableAsset = playerDie;

        if (scaleFromMicrophone != null)
        {
            scaleFromMicrophone.OnSoundVolumeChanged += SoundVolumeWarning;
        }

        mainCamera = Camera.main;
        //timelineCamera = playableDirector.GetGenericBinding(playerDie.GetOutputTrack(0)) as Camera;
    }

    private void Update()
    {
        if (monster != null)
        {
            PlayerTag();
        }
    }
    void PlayerTag()
    {
        if (monster.playerTag)
        {
            GameOver();
            StartCoroutine(LoadSceneWithDelay(currentSceneName, 1.5f));
        }
    }
    void SoundVolumeWarning(float loudness)
    {
        if (loudness >= maxSoundVolume)
        {
            count++;
            OnCountChanged?.Invoke(count);
            if (count == 4)
            {
                GameOver();
            }
        }
    }
    void GameOver()
    {
        //게임오버
        mainCamera.enabled = false;
        timelineCamera.enabled = true;
        playableDirector.Play();
    }

    private IEnumerator LoadSceneWithDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
