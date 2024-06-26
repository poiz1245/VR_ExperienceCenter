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
        //loudness = GetAveragedVolume() * loudnessSensibility; ����� �ҽ� Ȱ���� �� �̷��� ���µ�


        //loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;

        loudness = source.volume;

        if (loudness < threshold)
            loudness = 0;


        transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);
    }
    void GetThumbStickValue()
    {


    }
    /*float GetAveragedVolume() //����� �ҽ����� ������ �����͸� ��ġȭ �ϴ� �Լ��ε�
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
