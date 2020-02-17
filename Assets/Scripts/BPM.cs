using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPM : MonoBehaviour
{
    private static BPM bpmInstance;
    public float bpm;
    private float beatTimer, beatTimerD8;
    public static float beatInterval, beatIntervalD8;
    public static bool beatFull, beatD8;
    public static int beatCountFull, beatCountD8;

    public float[] tapTime = new float[4];
    public static int tap;
    public static bool customBeat;

    private void Awake()
    {
        if (bpmInstance != null && bpmInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            bpmInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Tapping();
        BeatDetection();
    }

    void BeatDetection()
    {
        //full beat count
        beatFull = false;
        beatInterval = 60.0f / bpm;
        beatTimer += Time.deltaTime;
        if (beatTimer >= beatInterval)
        {
            beatTimer -= beatInterval;
            beatFull = true;
            beatCountFull++;
        }
        //divided beat count
        beatD8 = false;
        beatIntervalD8 = beatInterval / 8;
        beatTimerD8 += Time.deltaTime;
        if (beatTimerD8 >= beatIntervalD8)
        {
            beatTimerD8 -= beatIntervalD8;
            beatD8 = true;
            beatCountD8++;
        }
    }

    void Tapping()
    {
     /*   if (Input.GetKeyUp(KeyCode.F1))
        {
            customBeat = true;
            tap = 0;
        }
        if (customBeat)
        {*/
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (tap < 4)
                {
                    tapTime[tap] = Time.realtimeSinceStartup;
                    tap++;
                }
                if (tap == 4)
                {
                    float averageTime = ((tapTime[1] - tapTime[0]) + (tapTime[2] - tapTime[1]) +
                                        (tapTime[3] - tapTime[2])) / 3;
                    bpm = (float)System.Math.Round((double)60 / averageTime, 2);
                    tap = 0;
                    beatTimer = 0;
                    beatTimerD8 = 0;
                    beatCountFull = 0;
                    customBeat = false;
                }
            }
        //}
    }
}
