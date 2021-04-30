using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlotDrawer : MonoBehaviour
{

    public LineRenderer lineRenderer;
    public int posCount = 5;
    public float lineWidth;
    public float waveStrenght = 1f;
    public float noiseStrenght = 1f;
    public float waveSpeed = 1f;
    public float amplitude = 20f;
    public int noiseAmplitude = 1;
    public float noise = -3f;
    float elapsedTime = 0f;


    void Start()
    {
        lineRenderer.useWorldSpace = false;
    }

    void Update()
    {
        lineRenderer.positionCount = posCount;

        elapsedTime += Time.deltaTime * waveSpeed;
        float widthMultiplier = (float)lineWidth / (float)posCount;
        
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float sinArg = (elapsedTime + i * widthMultiplier) * amplitude;
            float value = Mathf.Sin(sinArg) * waveStrenght + (i%noiseAmplitude == 0? Random.Range(-noise, noise) * noiseStrenght : 0);
            lineRenderer.SetPosition(i, new Vector3(i * widthMultiplier, value, 0));
        }
    }


}
