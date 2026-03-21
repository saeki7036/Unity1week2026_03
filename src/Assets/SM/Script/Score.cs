using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using unityroom.Api;

public class Score : MonoBehaviour
{
    static int score = 0;
    
    public static void ScoreReset() => score = 0;
    public static void AddScore(int add) => score += add;

    public static void SendScore()
    {
        UnityroomApiClient.Instance.SendScore(1,score,ScoreboardWriteMode.HighScoreDesc);
    }
}
