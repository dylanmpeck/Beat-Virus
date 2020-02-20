using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] float duration;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, duration * 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
