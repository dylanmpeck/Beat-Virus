using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTinySpheres : MonoBehaviour
{
    Vector3 origPos;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void OnEnable()
    {
        origPos = this.transform.position;
    }

    private void OnDisable()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        this.transform.position = origPos;
    }
}
