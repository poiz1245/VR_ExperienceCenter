using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleFromMicrophone : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] Vector3 minScale;
    [SerializeField] Vector3 maxScale;
    [SerializeField] AudioLoudnessDetection detector;

    public float loudnessSensibility = 100;
    public float threshold = 0.1f;

    float loudness;
    private void Start()
    {
    }
    void Update()
    {
        //loudness = GetAveragedVolume() * loudnessSensibility; 오디오 소스 활용할 때 이렇게 쓰는듯


        //loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;

        loudness = source.volume;

        if (loudness < threshold)
            loudness = 0;


        transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);
    }
    void GetThumbStickValue()
    {


    }
    /*float GetAveragedVolume() //오디오 소스에서 나오는 데이터를 수치화 하는 함수인듯
    {
        float[] data = new float[256];
        float a = 0;
        source.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }*/


}
