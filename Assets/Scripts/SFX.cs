using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFX : MonoBehaviour
{
    static AudioSource audioSource;

    [SerializeField] AudioClip shootClip;
    static AudioClip shootClipRef;
    [SerializeField] AudioClip burstClip;
    static AudioClip burstClipRef;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        shootClipRef = shootClip;
        burstClipRef = burstClip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayShootSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(shootClipRef);
    }

    public static void PlayBurstSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(burstClipRef);
    }
}
