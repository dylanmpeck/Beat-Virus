using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyOnAirTap : MonoBehaviour, IMixedRealityFocusHandler, IMixedRealityPointerHandler
{
    public string hand;
    [SerializeField] GameObject explodeSphere;
    [SerializeField] GameObject graphics;
    [SerializeField] GameObject hitText;

    [HideInInspector] public Material leftMaterial, rightMaterial;

    float errorMargin = .1f;

    private void Awake()
    {
        //CoreServices.InputSystem.RegisterHandler<IMixedRealityPointerHandler>(this.gameObject);
    }

    void IMixedRealityFocusHandler.OnFocusEnter(FocusEventData eventData)
    {
    }

    void IMixedRealityFocusHandler.OnFocusExit(FocusEventData eventData)
    {
    }

    void IMixedRealityPointerHandler.OnPointerDown(
         MixedRealityPointerEventData eventData)
    { }

    void IMixedRealityPointerHandler.OnPointerUp(MixedRealityPointerEventData eventData)
    {

    }

    void IMixedRealityPointerHandler.OnPointerDragged(
         MixedRealityPointerEventData eventData)
    { }

    void IMixedRealityPointerHandler.OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        if (eventData.Handedness.ToString() == hand)
        {
            float clickTime = Time.realtimeSinceStartup % BPM.beatInterval;
            if (clickTime >= 0.0f && clickTime <= errorMargin)
            {
                Instantiate(hitText, transform.position, Quaternion.identity);
                Debug.Log("A little late");
            }
            if (clickTime <= BPM.beatInterval && clickTime >= BPM.beatInterval - errorMargin)
            {
                Instantiate(hitText, transform.position, Quaternion.identity);
                Debug.Log("A little early");
            }
            //GameObject explode = Instantiate(explodeSphere, transform.position, transform.rotation);
            explodeSphere.SetActive(explodeSphere);
            // Debug.Log(this.gameObject.GetComponent<MeshRenderer>().materials[0].ToString());
            explodeSphere.GetComponent<ExplodeSphere>().sphereColor = GetComponentInChildren<MeshRenderer>().material;
            //Destroy(this.gameObject);
            GetComponent<Collider>().enabled = false;
            graphics.SetActive(false);
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
    }
}
