using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnAmplitude : MonoBehaviour
{
    public AudioPeer audioPeer;
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
            if (float.IsNaN(audioPeer.amplitude)) return;
            transform.localScale = new Vector3((audioPeer.amplitude * maxScale) + startScale,
                                               (audioPeer.amplitude * maxScale) + startScale,
                                               (audioPeer.amplitude * maxScale) + startScale);
            Color color = new Color(red * audioPeer.amplitude, green * audioPeer.amplitude, blue * audioPeer.amplitude);
            material.SetColor("_EmissionColor", color);
        }
        if (useBuffer)
        {
            if (float.IsNaN(audioPeer.amplitudeBuffer)) return;
            transform.localScale = new Vector3((audioPeer.amplitudeBuffer * maxScale) + startScale,
                                               (audioPeer.amplitudeBuffer * maxScale) + startScale,
                                               (audioPeer.amplitudeBuffer * maxScale) + startScale);
            Color color = new Color(red * audioPeer.amplitudeBuffer, green * audioPeer.amplitudeBuffer, blue * audioPeer.amplitudeBuffer);
            material.SetColor("_EmissionColor", color);
        }
    }
}
