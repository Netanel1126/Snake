using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWindow : MonoBehaviour
{
    private static ScoreWindow instence;
    private Text scoreText;

    private void Awake()
    {
        instence = this;
        int highscore = Score.GetHigeScore();
        scoreText = transform.Find("ScoreText").GetComponent<Text>();
        SetHighScoreText(highscore);
    }

    private void Update()
    {
        int score = Score.GetScore();
        scoreText.text = score.ToString();

        if (Score.TrySetNewHighScore())
        {
            SetHighScoreText(score);
        }
    }

    private void SetHighScoreText(int highscore)
    {
        transform.Find("HighScoreText").GetComponent<Text>().text = "HIGHSCORE\n" + highscore;

    }

    public static void HideStatic()
    {
        instence.gameObject.SetActive(false);
    }
}
