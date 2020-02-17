using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingBalls : MonoBehaviour
{
    [SerializeField] Transform child;
    AudioPeer audioPeer;
    float startScale, maxScale;
    bool useBuffer = true;
    public Material material;
    Color origColor;

    // Start is called before the first frame update
    void Start()
    {
        //material = GetComponent<MeshRenderer>().materials[0];
        child.GetComponent<MeshRenderer>().material = material;

        audioPeer = GameObject.Find("Audio Source").GetComponent<AudioPeer>();
        startScale = .2f;
        maxScale = .6f;

        origColor = material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (!useBuffer)
        {
            if (float.IsNaN(audioPeer.amplitude)) return;
            child.transform.localScale = new Vector3((audioPeer.amplitude * maxScale) + startScale,
                                               (audioPeer.amplitude * maxScale) + startScale,
                                               (audioPeer.amplitude * maxScale) + startScale);
            Color color = new Color(origColor.r * audioPeer.amplitude,  origColor.g * audioPeer.amplitude, origColor.b * audioPeer.amplitude);
            material.SetColor("_EmissionColor", color);
        }
        if (useBuffer)
        {
            if (float.IsNaN(audioPeer.amplitudeBuffer)) return;
            child.transform.localScale = new Vector3((audioPeer.amplitudeBuffer * maxScale) + startScale,
                                               (audioPeer.amplitudeBuffer * maxScale) + startScale,
                                               (audioPeer.amplitudeBuffer * maxScale) + startScale);
            Color color = new Color(origColor.r * audioPeer.amplitudeBuffer, origColor.g * audioPeer.amplitudeBuffer, origColor.b * audioPeer.amplitudeBuffer);
            material.SetColor("_EmissionColor", color);
        }
    }
}
