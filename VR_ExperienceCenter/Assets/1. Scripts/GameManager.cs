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
        if (loudness >= maxSoundVolume && soundWarningDelay <= 0) // �Ҹ� ũ�� ���� ������ 0���� ������
        {
            count++; //ī��Ʈ++

            if (hide != null)
            {
                hide.hideTime = 0; // ��� �Ҹ� ������ Ÿ�� �ʱ�ȭ
            }

            delay = 5f; // �⺻������ ������ ī��Ʈ�� �ߺ����� �����°� �����ϱ� ���ؼ� ������ �ٽ� �ʱ�ȭ
            soundWarningDelay = 1f; //�Ҹ� �ѹ������� ������ �����°� ������ ������

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
