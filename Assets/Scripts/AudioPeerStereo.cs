using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeerStereo : MonoBehaviour
{
    AudioSource audioSource;


    static float[] samplesLeft = new float[512];
    static float[] samplesRight = new float[512];

    // Audio spectrum info pulled from FFT algorithm
    static float[] freqBands = new float[8];
    static float[] bandBuffer = new float[8];
    float[] bufferDecrease = new float[8];

    // Frequency band info converted to value between 0 and 1
    float[] freqBandHighest = new float[8];
    public static float[] audioBand = new float[8];
    public static float[] audioBandBuffer = new float[8];

    public static float amplitude, amplitudeBuffer;
    float amplitudeHighest;

    public float audioProfile;

    public enum ChannelSelection { Stereo, Left, Right };

    public ChannelSelection channel;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetAudioProfile();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();
    }

    void SetAudioProfile()
    {
        for (int i = 0; i < 8; i++)
        {
            freqBandHighest[i] = audioProfile;
        }
    }

    void GetAmplitude()
    {
        float currentAmplitude = 0;
        float currentAmplitudeBuffer = 0;

        for (int i = 0; i < 8; i++)
        {
            currentAmplitude += audioBand[i];
            currentAmplitudeBuffer += audioBandBuffer[i];
        }
        if (currentAmplitude > amplitudeHighest)
        {
            amplitudeHighest = currentAmplitude;
        }
        amplitude = currentAmplitude / amplitudeHighest;
        amplitudeBuffer = currentAmplitudeBuffer / amplitudeHighest;
    }

    void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samplesLeft, 0, FFTWindow.Blackman);
        audioSource.GetSpectrumData(samplesRight, 1, FFTWindow.Blackman);
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (freqBands[i] > freqBandHighest[i])
            {
                freqBandHighest[i] = freqBands[i];
            }
            audioBand[i] = (freqBands[i] / freqBandHighest[i]);
            audioBandBuffer[i] = (bandBuffer[i] / freqBandHighest[i]);
        }
    }

    void BandBuffer()
    {
        for (int i = 0; i < 8; i++)
        {
            if (freqBands[i] > bandBuffer[i])
            {
                bandBuffer[i] = freqBands[i];
                bufferDecrease[i] = 0.005f;
            }
            if (freqBands[i] < bandBuffer[i])
            {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f;
            }
        }
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
                if (channel == ChannelSelection.Stereo)
                    average += (samplesLeft[count] + samplesRight[count]) * (count + 1);
                else if (channel == ChannelSelection.Left)
                    average += samplesLeft[count] * (count + 1);
                else if (channel == ChannelSelection.Right)
                    average += samplesRight[count] * (count + 1);
                count++;
            }

            average /= count;

            freqBands[i] = average * 10;
        }
    }
}
