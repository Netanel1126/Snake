using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;

public class PauseWindow : MonoBehaviour
{
    private static PauseWindow shareInstance;

    private void Awake()
    {
        shareInstance = this;

        transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        transform.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

        transform.Find("resumBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            GameHandler.ResumeGame();
        };

        transform.Find("mainMenuBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            Loader.Load(Loader.Scene.MainMenu);
        };

        SetupButtonsSounds();
        Hide();
    }

    private void SetupButtonsSounds()
    {
        transform.Find("mainMenuBtn").GetComponent<Button_UI>().AddButtonSounds();
        transform.Find("resumBtn").GetComponent<Button_UI>().AddButtonSounds();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public static void ShowStatic()
    {
        shareInstance.Show();
    }

    public static void HideStatic()
    {
        shareInstance.Hide();
    }
}
