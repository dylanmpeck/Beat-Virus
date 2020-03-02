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
/*    void Start()
    {
        //material = GetComponent<MeshRenderer>().materials[0];
        child.GetComponent<MeshRenderer>().material = material;

        audioPeer = GameObject.Find("Audio Source").GetComponent<AudioPeer>();
        startScale = .2f;
        maxScale = .45f;

        origColor = material.color;
    }*/

    private void OnEnable()
    {
        //material = GetComponent<MeshRenderer>().materials[0];
        if (material)
        {
            child.GetComponent<MeshRenderer>().material = material;
            origColor = material.color;
        }

        audioPeer = GameObject.Find("Audio Source").GetComponent<AudioPeer>();
        startScale = .2f;
        maxScale = .45f;


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
