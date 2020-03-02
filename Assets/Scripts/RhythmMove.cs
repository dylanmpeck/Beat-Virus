using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMove : MonoBehaviour
{
    [SerializeField] Transform circleIndicator;
    Transform target;
    Vector3 origPos;
    //Vector3 origCircleScale;
    float elapsedTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //origCircleScale = circleIndicator.localScale;
        origPos = transform.position;
        target = GameObject.FindGameObjectWithTag("RhythmTarget").GetComponent<Transform>();
    }

    private void OnEnable()
    {
        origPos = transform.position;
        elapsedTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(1f, 1f, 1f));

        if (BPM.beatInterval > 0.0f)
        {
            elapsedTime += Time.deltaTime;
            float lifeTime = elapsedTime / (BPM.beatInterval * 8.0f);
            transform.position = Vector3.Lerp(origPos, new Vector3(transform.position.x, transform.position.y, target.position.z), lifeTime);
           // circleIndicator.localScale = Vector3.Lerp(origCircleScale, new Vector3(.13f, .13f, .13f), lifeTime);
            if (elapsedTime >= BPM.beatInterval * 8 + .1f)
            {
               elapsedTime = 0.0f;
                Rigidbody rb;
                if (rb = GetComponent<Rigidbody>())
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
                this.gameObject.SetActive(false);

               // StartCoroutine(Deactivate());
            }
        }
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(1.2f);
        this.gameObject.SetActive(false);
    }
}
