using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public enum Waveform {sin, tri, sqr, saw, inv, noise};
    public Waveform waveform = Waveform.sin;

    public float baseStart = 0.0f;
    public float amplitude = 1.0f;
    public float phase = 0.0f;
    public float frequency = 0.5f;

    private Color originalColor;
    private Light ogLight;

    void Start()
    {
        ogLight = GetComponent<Light>();
        originalColor = ogLight.color;
    }

    void Update()
    {
       ogLight.color = originalColor * (EvalWave());
    }

    float EvalWave()
    {
        float x = (Time.time + phase) * frequency;
        float y;
        x = x - Mathf.Floor(x);

        if(waveform == Waveform.sin)
        {
            y = Mathf.Sin(x * 2 * Mathf.PI);
        }
        else if (waveform == Waveform.tri)
        {
            if(x < 0.5f)
            {
                y = 4.0f * x - 1.0f;
            }
            else
            {
                y = -4.0f * x + 1.0f;
            }
        }
        else
        {
            y = 1.0f;
        }

        return (y * amplitude) + baseStart;
    }

}
