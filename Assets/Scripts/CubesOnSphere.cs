using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesOnSphere : MonoBehaviour
{
    public GameObject cubePrefab;
    int numOfCubes = 12;

    // Start is called before the first frame update
    void Start()
    {
        float radius = GetComponent<SphereCollider>().radius;
        Vector3 origin = this.transform.position;

        for (int i = 0; i < numOfCubes; i++)
        {
            GameObject cube = GameObject.Instantiate(cubePrefab);

            //cube.transform.position = origin + Random.onUnitSphere * radius;

            float theta = (2 * Mathf.PI / numOfCubes) * i;

            cube.transform.position = new Vector3(radius * Mathf.Cos(theta) + origin.x, origin.y, radius * Mathf.Sin(theta) + origin.z);

            cube.transform.up = (cube.transform.position - origin).normalized;

            cube.transform.SetParent(this.transform);

            cube.GetComponent<FrequencyBandScale>().band = Random.Range(0, 8);
        }

        for (int i = 0; i < numOfCubes; i++)
        {
            GameObject cube = GameObject.Instantiate(cubePrefab);

            //cube.transform.position = origin + Random.onUnitSphere * radius;

            float theta = (2 * Mathf.PI / numOfCubes) * i;

            cube.transform.position = new Vector3(origin.x, radius * Mathf.Cos(theta) + origin.y, radius * Mathf.Sin(theta) + origin.z);

            cube.transform.up = (cube.transform.position - origin).normalized;

            cube.transform.SetParent(this.transform);

            cube.GetComponent<FrequencyBandScale>().band = Random.Range(0, 8);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
