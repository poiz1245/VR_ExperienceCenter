using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;
using static ScaleFromMicrophone;

public class ScaleFromMicrophone : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] Vector3 minScale;
    [SerializeField] Vector3 maxScale;
    [SerializeField] AudioLoudnessDetection detector;
    [SerializeField] GameObject warningImage;

    public float loudnessSensibility = 100;
    public float threshold = 0.1f;

    float loudness;
        
    public delegate void SoundVolumeChanged(float loudness);
    public event SoundVolumeChanged OnSoundVolumeChanged;

    void Update()
    {
        loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;

        if (loudness < threshold)
            loudness = 0;

        OnSoundVolumeChanged?.Invoke(loudness);
        warningImage.transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);


        //loudness = GetAveragedVolume() * loudnessSensibility; ����� �ҽ� Ȱ���� �� �̷��� ���µ�
        //loudness = source.volume;
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
