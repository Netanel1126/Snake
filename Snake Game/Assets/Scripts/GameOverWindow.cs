using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour
{
    private static GameOverWindow shareInstance;

    private void Awake()
    {
        shareInstance = this;
      
        transform.Find("retryBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            Loader.Load(Loader.Scene.MyGameScene);
        };

        Hide();
    }

    private void Show(bool isNewHighScore)
    {
        gameObject.SetActive(true);
        transform.Find("newHighScoreText").gameObject.SetActive(isNewHighScore);
        transform.Find("scoreText").GetComponent<Text>().text = Score.GetScore() + "";
        transform.Find("highScoreText").GetComponent<Text>().text = "HIGHSCORE " + Score.GetHigeScore();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public static void ShowStatic(bool isNewHighScore)
    {
        shareInstance.Show(isNewHighScore);
    }
}
