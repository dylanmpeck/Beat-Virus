using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class DestroyOnAirTap : MonoBehaviour, IMixedRealityFocusHandler, IMixedRealityPointerHandler
{
    public string hand;
    [SerializeField] GameObject explodeSphere;
    [SerializeField] GameObject graphics;
    [SerializeField] GameObject hitText;
    [SerializeField] GameObject purpleShot;
    [SerializeField] GameObject greenShot;

    [HideInInspector] public Material leftMaterial, rightMaterial;

    bool clicked;

    float errorMargin = .13f;

    int maxScoreForBall = 50;

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
            if (clicked) return;
            clicked = true;

            float clickTime = (AudioPeer.timer - .1f) % BPM.beatInterval;
           // Debug.Log(clickTime);
            if (clickTime >= 0.0f && clickTime <= errorMargin)
            {
                GameManager.IncreaseCombo();
                StartCoroutine(SpawnAfterDelay(.18f));
                Debug.Log("A little late");
            }
            else if (clickTime <= BPM.beatInterval && clickTime >= BPM.beatInterval - errorMargin)
            {
                GameManager.IncreaseCombo();
                StartCoroutine(SpawnAfterDelay(.18f));
                Debug.Log("A little early");
            }
            else
            {
                GameManager.comboCount = 0;
                GameManager.IncreaseCombo();
            }

            SFX.PlayShootSound();

            CalculateScore(clickTime);

            GetComponent<Collider>().enabled = false;
            Handedness myHand = (hand == "Right") ? Handedness.Right : Handedness.Left;
            GameObject projectile = (hand == "Right") ? purpleShot : greenShot;
            
            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, myHand, out MixedRealityPose pose))
            {
                StartCoroutine(ExplodeBallAfterProjectileAnim(projectile, pose.Position));
            }
            else
            {
                XRNode handNode = (hand == "Right") ? XRNode.RightHand : XRNode.LeftHand;
                StartCoroutine(ExplodeBallAfterProjectileAnim(projectile, InputTracking.GetLocalPosition(handNode) + new Vector3(0.0f, 0.0f, .5f)));
            }
        }
    }

    IEnumerator ExplodeBallAfterProjectileAnim(GameObject laser, Vector3 handPos)
    {
        GameObject projectile = Instantiate(laser, handPos, Quaternion.identity);
        projectile.GetComponentInChildren<particleAttractorLinear>().target = this.gameObject.transform;

        //bool onBeat = false;

        yield return new WaitForSeconds(laser.GetComponent<ParticleSystem>().main.duration + .1f);

        // Regular destroy code

        //GameObject explode = Instantiate(explodeSphere, transform.position, transform.rotation);
        explodeSphere.SetActive(explodeSphere);
        // Debug.Log(this.gameObject.GetComponent<MeshRenderer>().materials[0].ToString());
        explodeSphere.GetComponent<ExplodeSphere>().sphereColor = GetComponentInChildren<MeshRenderer>().material;
        //Destroy(this.gameObject);
        graphics.SetActive(false);
        SFX.PlayBurstSound();
        StartCoroutine(Destroy());
    }

    void CalculateScore(float clickTime)
    {
        float scorePercentage;
        if (clickTime >= 0.0f && clickTime < BPM.beatInterval / 2.0f)
            scorePercentage = (BPM.beatInterval - clickTime) / BPM.beatInterval;
        else
            scorePercentage = (BPM.beatInterval - (BPM.beatInterval - clickTime)) / BPM.beatInterval;

        GameManager.Scored((int)(maxScoreForBall * scorePercentage));
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
    }

    IEnumerator SpawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(hitText, transform.position, Quaternion.identity);
    }
}
