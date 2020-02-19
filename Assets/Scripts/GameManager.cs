using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int maxHP;

    static int currentHP;

    #region FadeToBlack Variables
    [SerializeField] GameObject blackScreen;
    MeshRenderer bsMesh;
    [SerializeField] GameObject Tunnel;
    [SerializeField] GameObject RhythmGenerator;
    [SerializeField] GameObject WinScreen;
    [SerializeField] TextMesh endScreenText;
    #endregion

    bool end = false;

    void Start()
    {
        currentHP = maxHP;
        bsMesh = blackScreen.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player made it to end.
        if (AudioPeer.mainSong.isPlaying == false && end == false)
        {
            end = true;
            StartCoroutine(FadeToBlack("You Win!"));
        }
    }

    IEnumerator FadeToBlack(string _endScreenText)
    {
        float a = bsMesh.material.color.a;
        float elapsedTime = 0.0f;
        while (bsMesh.material.color.a < 1.0f)
        {
            elapsedTime += Time.deltaTime;
            a = Mathf.Lerp(a, 1.0f, elapsedTime / 3.0f);
            bsMesh.material.color = new Color(bsMesh.material.color.r, bsMesh.material.color.g, bsMesh.material.color.b, a);
            yield return null;
        }

        endScreenText.text = _endScreenText;

        Tunnel.SetActive(false);
        RhythmGenerator.SetActive(false);
        blackScreen.SetActive(false);
        WinScreen.SetActive(true);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            StartCoroutine(FadeToBlack("You were Infected."));
        }
    }
}
