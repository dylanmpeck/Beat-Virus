using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int maxHP;

    static int currentHP;

    public static int comboCount = 1;
    public static int comboMult = 1;
    public static int score = 0;

    [SerializeField] TextMesh comboText;
    static TextMesh comboTextRef;
    [SerializeField] TextMesh scoreText;
    static TextMesh scoreTextRef;

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
        scoreTextRef = scoreText;
        scoreTextRef.text = score.ToString();
        comboTextRef = comboText;
        comboTextRef.text = "x" + comboCount.ToString();
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
        blackScreen.SetActive(true);
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

    public static void Scored(int addScore)
    {
        score += addScore * comboMult;
        scoreTextRef.text = score.ToString();
    }

    public static void IncreaseCombo()
    {
        comboCount++;
        if (comboCount == 1)
        {
            comboTextRef.text = "x" + comboCount.ToString();
            comboMult = 1;
        }
        else if (comboCount == 2)
        {
            comboTextRef.text = "x" + comboCount.ToString();
            comboMult = 2;
        }
        else if (comboCount == 4)
        {
            comboTextRef.text = "x" + comboCount.ToString();
            comboMult = 4;
        }
        else if (comboCount == 8)
        {
            comboTextRef.text = "x" + comboCount.ToString();
            comboMult = 8;
        }
    }
}
