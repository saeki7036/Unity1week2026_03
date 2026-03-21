using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using unityroom.Api;

public class Score : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        this.gameObject.SetActive(score != 0);

        textMeshProUGUI.SetText(score.ToString());
    }

    static int score = 0;
    
    public static void ScoreReset() => score = 0;
    public static void AddScore(int add) => score += add;

    public static void SendScore()
    {
        UnityroomApiClient.Instance.SendScore(1,score,ScoreboardWriteMode.HighScoreDesc);
    }
}
