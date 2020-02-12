using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnAmplitude : MonoBehaviour
{
    public float startScale, maxScale;
    public bool useBuffer;
    Material material;
    public float red, green, blue;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().materials[0];    
    }

    // Update is called once per frame
    void Update()
    {
        if (!useBuffer)
        {
            transform.localScale = new Vector3((AudioPeer.amplitude * maxScale) + startScale,
                                               (AudioPeer.amplitude * maxScale) + startScale,
                                               (AudioPeer.amplitude * maxScale) + startScale);
            Color color = new Color(red * AudioPeer.amplitude, green * AudioPeer.amplitude, blue * AudioPeer.amplitude);
            material.SetColor("_EmissionColor", color);
        }
        if (useBuffer)
        {
            transform.localScale = new Vector3((AudioPeer.amplitudeBuffer * maxScale) + startScale,
                                               (AudioPeer.amplitudeBuffer * maxScale) + startScale,
                                               (AudioPeer.amplitudeBuffer * maxScale) + startScale);
            Color color = new Color(red * AudioPeer.amplitudeBuffer, green * AudioPeer.amplitudeBuffer, blue * AudioPeer.amplitudeBuffer);
            material.SetColor("_EmissionColor", color);
        }
    }
}
