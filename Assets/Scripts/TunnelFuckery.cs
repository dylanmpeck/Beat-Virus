using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelFuckery : MonoBehaviour
{
    public AudioPeer audioPeer;
    public int band;
    public float startScale, scaleMultiplier;
    public bool useBuffer;
    public bool brightLight;
    Material material;
    Color origColor;

    float lerpTime = 1f;
    float currentLerpTime;
    bool flipLerp;
    int slicesMax;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Kvant.Tunnel>().material;
        origColor = material.color;
        slicesMax = GetComponent<Kvant.Tunnel>().slices;
    }

    // Update is called once per frame
    void Update()
    {
        if (float.IsNaN(audioPeer.audioBandBuffer[band])) return;
        //transform.localScale = new Vector3(transform.localScale.x,
        //                       (audioPeer.audioBandBuffer[band] * scaleMultiplier) + startScale,
        //                        transform.localScale.z);

        GetComponent<Kvant.Tunnel>().noiseWarp = audioPeer.audioBandBuffer[band];

        Color color;
        if (brightLight)
            color = new Color(audioPeer.audioBandBuffer[band], audioPeer.audioBandBuffer[band], audioPeer.audioBandBuffer[band]);
        else
        {
            float r = Mathf.Clamp(origColor.r * audioPeer.audioBandBuffer[band], 0, 1);
            float g = Mathf.Clamp(origColor.g * audioPeer.audioBandBuffer[band], 0, 1);
            float b = Mathf.Clamp(origColor.b * audioPeer.audioBandBuffer[band], 0, 1);
            color = new Color(r, g, b);
        }
        material.SetColor("_Emission", color);

        RotateTunnel();

        if (flipLerp)
            currentLerpTime -= Time.deltaTime;
        else
            currentLerpTime += Time.deltaTime;
        currentLerpTime = Mathf.Clamp(currentLerpTime, 0.0f, lerpTime);
        if (currentLerpTime == 0.0f || currentLerpTime == lerpTime)
            flipLerp = !flipLerp;

        float perc = currentLerpTime / lerpTime;
        GetComponent<Kvant.Tunnel>().noiseElevation = Mathf.Lerp(.5f, 0.8f, perc);

        CreateRainbowEffect(new Vector3(1.666f, 2.666f, 3.666f), new Vector3(0, 0, 0));
        GetComponent<Kvant.Tunnel>().lineColor = Color.Lerp(Color.black, Color.red, audioPeer.audioBandBuffer[band]);
    }

    void RotateTunnel()
    {
        this.transform.eulerAngles = new Vector3(0.0f, Mathf.Sin(Time.time * GetComponent<Kvant.TunnelScroller>().speed), 0f);
    }

    void CreateRainbowEffect(Vector3 frequencies, Vector3 phases)
    {
        float r = Mathf.Sin(frequencies.x * Time.time + phases.x) * 127 + 128;
        float g = Mathf.Sin(frequencies.y * Time.time + phases.y) * 127 + 128;
        float b = Mathf.Sin(frequencies.x * Time.time + phases.z) * 127 + 128;

        r = r / 255;
        g = g / 255;
        b = b / 255;

        material.color = new Color(r, g, b);
    }
}
