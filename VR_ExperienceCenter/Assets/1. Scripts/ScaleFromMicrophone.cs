using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleFromMicrophone : MonoBehaviour
{
    //public AudioSource source;
    /*public Vector3 minScale;
    public Vector3 maxScale;*/
    [SerializeField] float minValue;
    [SerializeField] float maxValue;
    public AudioLoudnessDetection detector;

    Slider slider;

    public float loudnessSensibility = 100;
    public float threshold = 0.1f;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    void Update()
    {
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;

        if (loudness < threshold)
            loudness = 0;

        slider.value = loudness;

        //transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);
    }
}