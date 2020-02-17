using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMove : MonoBehaviour
{
    Transform target;
    Vector3 origPos;
    float elapsedTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        origPos = transform.position;
        target = GameObject.FindGameObjectWithTag("RhythmTarget").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(1f, 1f, 1f));

        if (BPM.beatInterval > 0.0f)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(origPos, new Vector3(transform.position.x, transform.position.y, target.position.z), elapsedTime / (BPM.beatInterval * 8.0f));
            if (elapsedTime >= BPM.beatInterval * 8 + .1f)
            {
                Destroy(this.gameObject);
                //elapsedTime = 0.0f;
                //transform.position = origPos;
            }

        }
    }
}
