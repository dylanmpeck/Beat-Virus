using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyBandScale : MonoBehaviour
{
    public int band;
    public float startScale, scaleMultiplier;
    public bool useBuffer;
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
            transform.localScale = new Vector3(transform.localScale.x,
                                   (AudioPeer.audioBandBuffer[band] * scaleMultiplier) + startScale,
                                    transform.localScale.z);
            Color color = new Color(AudioPeer.audioBandBuffer[band], AudioPeer.audioBandBuffer[band], AudioPeer.audioBandBuffer[band]);
            //float r = Mathf.Clamp(origColor.r * AudioPeer.audioBandBuffer[band], 0, 1);
            //float g = Mathf.Clamp(origColor.g * AudioPeer.audioBandBuffer[band], 0, 1);
            //float b = Mathf.Clamp(origColor.b * AudioPeer.audioBandBuffer[band], 0, 1);
            //Color color = new Color(r, g, b);
            material.SetColor("_EmissionColor", color);
        }
        if (!useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x,
                                   (AudioPeer.audioBand[band] * scaleMultiplier) + startScale,
                                    transform.localScale.z);
            Color color = new Color(AudioPeer.audioBand[band], AudioPeer.audioBand[band], AudioPeer.audioBand[band]);
            material.SetColor("_EmissionColor", color);
        }

   
    }
}
