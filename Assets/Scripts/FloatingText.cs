using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    TextMesh tm;
    Color color;
    float moveSpeed = .5f;
    float transparentSpeed = 2f;
    float duration = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        tm = GetComponent<TextMesh>();
        color = tm.color;
        Destroy(this.gameObject, duration);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.0f, moveSpeed * Time.deltaTime, 0.0f, Space.World);
        color.a -= transparentSpeed * Time.deltaTime;
        tm.color = color;
    }
}
