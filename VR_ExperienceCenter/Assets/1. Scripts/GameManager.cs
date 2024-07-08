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
    [SerializeField] Monster outSideMonster;
    [SerializeField] CloseOpenDoor lastDoor;
    [SerializeField] GameObject fadeOut;

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

        if (monster != null || outSideMonster != null)
        {
            PlayerTag();
        }
        if (count >= 4)
        {
            GameOver();
        }

        if (lastDoor != null && lastDoor.isOpen)
        {
            GameClear();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StageStart();
        }
    }
    void GameClear()
    {
        fadeOut.SetActive(true);
        scaleFromMicrophone.OnSoundVolumeChanged -= SoundVolumeWarning;
        chaseStart = false;
        count = 0;

        StartCoroutine(LoadScene());

    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Ending");
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
        if (monster != null && monster.playerTag)
        {
            GameOver();
        }

        if (outSideMonster != null && outSideMonster.playerTag)
        {
            GameOver();
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

            /*  if (count == 4)
              {
                  GameOver();
              }*/
        }
    }
    void GameOver()
    {
        if (scaleFromMicrophone != null)
        {
            scaleFromMicrophone.OnSoundVolumeChanged -= SoundVolumeWarning;
        }
        mainCamera.enabled = false;
        timelineCamera.enabled = true;
        playableDirector.Play();
        StartCoroutine(LoadSceneWithDelay(currentSceneName, 2f));
    }
    private IEnumerator LoadSceneWithDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
