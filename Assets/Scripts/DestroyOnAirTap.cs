using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyOnAirTap : MonoBehaviour, IMixedRealityFocusHandler, IMixedRealityPointerHandler
{
    public string hand;
    [SerializeField] GameObject explodeSphere;

    [HideInInspector] public Material leftMaterial, rightMaterial;

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
            GameObject explode = Instantiate(explodeSphere, transform.position, transform.rotation);

            // Debug.Log(this.gameObject.GetComponent<MeshRenderer>().materials[0].ToString());
            explode.GetComponent<ExplodeSphere>().sphereColor = GetComponentInChildren<MeshRenderer>().material;
            Destroy(this.gameObject);
        }
    }
}
