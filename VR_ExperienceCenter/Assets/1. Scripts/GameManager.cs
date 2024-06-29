using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] ScaleFromMicrophone scaleFromMicrophone;
    [SerializeField] float maxSoundVolume;
    [SerializeField] TimelineAsset playerDie;
    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] Camera timelineCamera;

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
        scaleFromMicrophone.OnSoundVolumeChanged += SoundVolumeWarning;

        mainCamera = Camera.main;
        //timelineCamera = playableDirector.GetGenericBinding(playerDie.GetOutputTrack(0)) as Camera;
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
                mainCamera.enabled = false;
                timelineCamera.enabled = true;
            }
        }
    }
    void GameOver()
    {
        if (count == 4)
        {
            //게임오버
            playableDirector.Play();
        }
    }
}
