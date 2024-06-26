using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderFromMicrophone : MonoBehaviour
{
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
    }
}