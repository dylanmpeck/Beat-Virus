using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCubes : MonoBehaviour
{
    public GameObject sampleCubePrefab;

    GameObject[] sampleCube = new GameObject[512];

    public float maxScale;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 512; i++)
        {
            GameObject cubeInstance = GameObject.Instantiate(sampleCubePrefab);
            cubeInstance.transform.position = this.transform.position;
            cubeInstance.transform.SetParent(this.transform);
            cubeInstance.name = "SampleCube" + i;
            this.transform.rotation = Quaternion.Euler(0, (360.0f / 512.0f) * i, 0);

            cubeInstance.transform.position = Vector3.forward * 100;
            sampleCube[i] = cubeInstance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 512; i++)
        {
            if (sampleCube[i] != null)
            {
                sampleCube[i].transform.localScale = new Vector3(10, (AudioPeer.audioBand[i] * maxScale) * 2, 10);
            }
        }
    }
}
