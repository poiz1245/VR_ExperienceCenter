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
    [SerializeField] MonsterChase monster;

    [Header("SoundWarning")]
    [SerializeField] ScaleFromMicrophone scaleFromMicrophone;
    [SerializeField] Hide hide;
    public float maxSoundVolume;
    public bool chaseStart = false;
    public int count;

    [Header("TimeLine")]
    [SerializeField] TimelineAsset playerDie;
    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] Camera timelineCamera;

  

    Camera mainCamera;

    float soundWarningDelay = 1f;
    float delay = 5f;

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
        mainCamera = Camera.main;
    }

    private void Update()
    {
        soundWarningDelay -= Time.deltaTime;

        if (chaseStart)
        {
            ChaseStart();
        }

        if (monster != null)
        {
            PlayerTag();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StageStart();
        }
    }
    void StageStart()
    {
        if (scaleFromMicrophone != null)
        {
            scaleFromMicrophone.OnSoundVolumeChanged += SoundVolumeWarning;
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
    void ChaseStart()
    {
        delay -= Time.deltaTime;

        if (delay <= 0)
        {
            count++;
            delay = 5f;
        }
    }
    void SoundVolumeWarning(float loudness)
    {
        if (loudness >= maxSoundVolume && soundWarningDelay <= 0) // 소리 크게 내고 딜레이 0보다 작으면
        {
            count++; //카운트++

            if (hide != null)
            {
                hide.hideTime = 0; // 숨어서 소리 지르면 타임 초기화
            }

            delay = 5f; // 기본적으로 오르는 카운트가 중복으로 오르는거 방지하기 위해서 딜레이 다시 초기화
            soundWarningDelay = 1f; //소리 한번지를때 여러번 오르는거 방지용 딜레이

            chaseStart = true;

            OnCountChanged?.Invoke(count);

            if (count == 4)
            {
                GameOver();
            }
        }
    }
    void GameOver()
    {
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
