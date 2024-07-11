using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public enum Waveform { sin, tri, sqr, saw, inv, noise }
    public Waveform waveform = Waveform.sin;

    public float baseIntensity = 0.8f; // Minimum light intensity
    public float amplitude = 0.2f; // Amplitude of the waveform
    public float phase = 1.5f;
    public float frequency = 0.2f;

    private Color originalColor;
    private Light ogLight;

    void Start()
    {
        ogLight = GetComponent<Light>();
        originalColor = ogLight.color;
    }

    void Update()
    {
        ogLight.color = originalColor * Mathf.Clamp(baseIntensity + EvalWave(), 0.5f, 1.5f); // Clamp the result to keep it in a desirable range
    }

    float EvalWave()
    {
        float time = (Time.time + phase) * frequency;
        float x = time - Mathf.Floor(time);
        float y = 0f;

        switch (waveform)
        {
            case Waveform.sin:
                y = Mathf.Sin(x * 2 * Mathf.PI);
                break;
            case Waveform.tri:
                y = x < 0.5f ? 4.0f * x - 1.0f : -4.0f * x + 3.0f;
                break;
            case Waveform.sqr:
                y = x < 0.5f ? 1.0f : -1.0f;
                break;
            case Waveform.saw:
                y = 2.0f * x - 1.0f;
                break;
            case Waveform.inv:
                y = 1.0f - 2.0f * x;
                break;
            case Waveform.noise:
                y = 1.0f - 2.0f * Random.value;
                break;
        }

        return y * amplitude;
    }
}
