using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleIndicator : MonoBehaviour
{
    Vector3 origScale;
    [SerializeField] Sprite missSprite, hitSprite;
    float errorMargin = .13f;
    // Start is called before the first frame update
    void Start()
    {
        origScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float clickTime = (AudioPeer.timer) % BPM.beatInterval;
        float percentOfBeat = clickTime / BPM.beatInterval;
        if (percentOfBeat < .5f)
            percentOfBeat = 1.0f - percentOfBeat;
        transform.localScale = Vector3.Lerp(origScale, new Vector3(.13f, .13f, .13f), percentOfBeat);

        if ((clickTime >= 0.0f && clickTime <= errorMargin) ||
            clickTime <= BPM.beatInterval && clickTime >= BPM.beatInterval - errorMargin)
            GetComponent<SpriteRenderer>().sprite = hitSprite;
        else
            GetComponent<SpriteRenderer>().sprite = missSprite;

        //GetComponent<SpriteRenderer>().color = Color.Lerp(Color.red, Color.green, percentOfBeat);
    }
}
