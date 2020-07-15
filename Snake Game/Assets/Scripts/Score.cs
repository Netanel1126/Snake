using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score
{
    private static int score;
    private static bool isNewHighScore = false;

    public static void InitializeStatic()
    {
        score = 0;
        isNewHighScore = false;
        Time.timeScale = 1f;
    }

    public static int GetScore()
    {
        return score;
    }

    public static void AddScore()
    {
        score += 100;
    }

    public static int GetHigeScore()
    {
        return PlayerPrefs.GetInt("highscore",0);
    }

    public static void SetHighScore(int score)
    {
        PlayerPrefs.SetInt("highscore", score);
        PlayerPrefs.Save();
    }

    public static bool TrySetNewHighScore()
    {
        return TrySetNewHighScore(score);
    }


    private static bool TrySetNewHighScore(int score)
    {
        if (score > GetHigeScore())
        {
            SetHighScore(score);
            isNewHighScore = true;
            return true;
        }
        return false;
    }

    public static bool IsNewHighScore
    {
        get => isNewHighScore;
    }
}
