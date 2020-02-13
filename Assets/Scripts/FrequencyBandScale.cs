using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyBandScale : MonoBehaviour
{
    public AudioPeer audioPeer;
    public int band;
    public float startScale, scaleMultiplier;
    public bool useBuffer;
    public bool brightLight;
    Material material;
    Color origColor;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().materials[0];
        origColor = material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (useBuffer)
        {
            if (float.IsNaN(audioPeer.audioBandBuffer[band])) return;
            transform.localScale = new Vector3(transform.localScale.x,
                                   (audioPeer.audioBandBuffer[band] * scaleMultiplier) + startScale,
                                    transform.localScale.z);
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
            material.SetColor("_EmissionColor", color);
        }
        if (!useBuffer)
        {
            if (float.IsNaN(audioPeer.audioBand[band])) return;
            transform.localScale = new Vector3(transform.localScale.x,
                                   (audioPeer.audioBand[band] * scaleMultiplier) + startScale,
                                    transform.localScale.z);
            Color color = new Color(audioPeer.audioBand[band], audioPeer.audioBand[band], audioPeer.audioBand[band]);
            material.SetColor("_EmissionColor", color);
        }
    }
}
