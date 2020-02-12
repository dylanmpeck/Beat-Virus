﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
    AudioSource audioSource;
    public static float[] samples = new float[512];
    public static float[] freqBands = new float[8];

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
    }

    void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    void MakeFrequencyBands()
    {
        /*
         * 22050 / 512 = 43 hertz per sample
         * 
         * Sample Frequency Bands
         * 20 - 60 hz
         * 60 - 250 hz
         * 250 - 500 hz
         * 500 - 2000 hz
         * 2000 - 4000 hz
         * 4000 - 6000 hz
         * 6000 - 20000 hz
         * 
         * 0 - 2 samples = 86hz (0 - 86)
         * 1 - 4 samples = 172hz (87 - 258)
         * 2 - 8 samples = 344hz (259 - 602)
         * 3 - 16 samples = 688hz (603 - 1290)
         * 4 - 32 samples = 1376hz (1291 - 2666)
         * 5 - 64 samples = 2752hz (2667 - 5418)
         * 6 - 128 samples = 5504hz (5419 - 10922)
         * 7 - 256 samples = 11008hz (10923 - 21930)
         * 510 samples total (maybe add 2 more to the last frequency band)
         */

        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
                sampleCount += 2; // gives us 512 samples total

            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;

            freqBands[i] = average * 10;
        }
    }
}
