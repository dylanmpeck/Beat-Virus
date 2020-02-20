using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Microsoft.MixedReality.Toolkit;

public class HoverOverController : MonoBehaviour
{
    [SerializeField] bool VR;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!VR)
            return;

        this.transform.position = InputTracking.GetLocalPosition(XRNode.RightHand) + new Vector3(0.0f, 0.1f, 0.0f);
    }
}
