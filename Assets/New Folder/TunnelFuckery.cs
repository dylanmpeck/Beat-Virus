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
    public Color[] tunnelColors;

    float lerpTime = 1f;
    float currentLerpTime;
    bool flipLerp;
    int slicesMax;

    float origHeight;
    float heightLerpTime = 5f;
    float currentHeightLerpTime;
    bool flipHeightLerp;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Kvant.Tunnel>().material;
        origColor = material.color;
        slicesMax = GetComponent<Kvant.Tunnel>().slices;

        origHeight = GetComponent<Kvant.Tunnel>().height;

        this.transform.position = Camera.main.transform.position;
        this.transform.rotation = Camera.main.transform.rotation;
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
            float r = Mathf.Clamp(origColor.r * audioPeer.audioBandBuffer[band], 0, .75f);
            float g = Mathf.Clamp(origColor.g * audioPeer.audioBandBuffer[band], 0, .75f);
            float b = Mathf.Clamp(origColor.b * audioPeer.audioBandBuffer[band], 0, .75f);
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

        if (flipHeightLerp)
            currentHeightLerpTime -= Time.deltaTime;
        else
            currentHeightLerpTime += Time.deltaTime;
        currentHeightLerpTime = Mathf.Clamp(currentLerpTime, 0.0f, heightLerpTime);
        if (currentHeightLerpTime == 0.0f || currentHeightLerpTime == heightLerpTime)
            flipHeightLerp = !flipHeightLerp;

        float perc = currentLerpTime / lerpTime;
       // GetComponent<Kvant.Tunnel>().noiseElevation = Mathf.Lerp(.5f, 0.8f, perc);

        float heightPerc = currentHeightLerpTime / heightLerpTime;
        // GetComponent<Kvant.Tunnel>().height = Mathf.Lerp(origHeight, origHeight + 5.0f, origHeight + (10.0f * audioPeer.audioBandBuffer[band]));

        //CreateRainbowEffect(new Vector3(1.666f, 2.666f, 3.666f), new Vector3(0, 0, 0));
        CreateRainbowEffect(new Vector3(BPM.beatInterval * 2, BPM.beatInterval * 2, BPM.beatInterval * 2), new Vector3(2 * Mathf.PI / 3, 2 * Mathf.PI / 3, 4 * Mathf.PI / 3));
        //material.color = GetPointOnBezierCurve(tunnelColors[0], tunnelColors[1], tunnelColors[2], tunnelColors[3], audioPeer.audioBandBuffer[band]);
        GetComponent<Kvant.Tunnel>().lineColor = Color.Lerp(Color.black, Color.red, audioPeer.audioBandBuffer[band]);
    }

    void RotateTunnel()
    {
        this.transform.eulerAngles = new Vector3(0.0f, Mathf.Sin(Time.time * GetComponent<Kvant.TunnelScroller>().speed), 0f);
    }

    void CreateRainbowEffect(Vector3 frequencies, Vector3 phases)
    {
        float r = Mathf.Sin(frequencies.x * Time.time + phases.x) * 32 + 128;
        float g = Mathf.Sin(frequencies.y * Time.time + phases.y) * 127 + 128;
        float b = Mathf.Sin(frequencies.x * Time.time + phases.z) * 127 + 128;

        r = r / 255;
        g = g / 255;
        b = b / 255;

        Color color = new Color(r, g, b);
                float h, s, v;
                Color.RGBToHSV(color, out h, out s, out v);

                s = Mathf.Clamp(s, 0.3f, .85f);
                v = Mathf.Clamp(v, 0.3f, .85f);

        //material.color = color;

        material.color = Color.HSVToRGB(h, s, v);
    }

    Color GetPointOnBezierCurve(Color p0, Color p1, Color p2, Color p3, float t)
    {
        float u = 1f - t;
        float t2 = t * t;
        float u2 = u * u;
        float u3 = u2 * u;
        float t3 = t2 * t;

        Color result =
            (u3) * p0 +
            (3f * u2 * t) * p1 +
            (3f * u * t2) * p2 +
            (t3) * p3;

        return result;
    }
}
