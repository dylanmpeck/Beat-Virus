using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesOnSphere : MonoBehaviour
{
    public GameObject cubePrefab;
    int numOfCubes = 12;
    public AudioPeer audioPeer;

    // Start is called before the first frame update
    void Start()
    {
        audioPeer = GameObject.FindObjectOfType<AudioPeer>();
        float radius = GetComponent<SphereCollider>().radius;
        Vector3 origin = this.transform.position;

        for (int i = 0; i < numOfCubes; i++)
        {
            GameObject cube = GameObject.Instantiate(cubePrefab);
            cube.GetComponent<FrequencyBandScale>().audioPeer = audioPeer;

            //cube.transform.position = origin + Random.onUnitSphere * radius;

            float theta = (2 * Mathf.PI / numOfCubes) * i;

            cube.transform.position = new Vector3(radius * Mathf.Cos(theta) + origin.x, origin.y, radius * Mathf.Sin(theta) + origin.z);

            cube.transform.up = (cube.transform.position - origin).normalized;

            cube.transform.SetParent(this.transform);

            cube.GetComponent<FrequencyBandScale>().band = Random.Range(0, 8);

            cube.GetComponent<FrequencyBandScale>().brightLight = true;
        }

        for (int i = 0; i < numOfCubes; i++)
        {
            GameObject cube = GameObject.Instantiate(cubePrefab);
            cube.GetComponent<FrequencyBandScale>().audioPeer = audioPeer;
            //cube.transform.position = origin + Random.onUnitSphere * radius;

            float theta = (2 * Mathf.PI / numOfCubes) * i;

            cube.transform.position = new Vector3(origin.x, radius * Mathf.Cos(theta) + origin.y, radius * Mathf.Sin(theta) + origin.z);

            cube.transform.up = (cube.transform.position - origin).normalized;

            cube.transform.SetParent(this.transform);

            cube.GetComponent<FrequencyBandScale>().band = Random.Range(0, 8);

            cube.GetComponent<FrequencyBandScale>().brightLight = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
