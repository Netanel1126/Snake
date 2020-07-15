using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;

public class MainMenuWindow : MonoBehaviour
{

    private enum SubMenu
    {
        MainSub,
        howToPlaySub,
    }

    private void Awake()
    {
        transform.Find(SubMenu.howToPlaySub.ToString()).GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        transform.Find(SubMenu.MainSub.ToString()).Find("playBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            Loader.Load(Loader.Scene.MyGameScene);
        };

        transform.Find(SubMenu.MainSub.ToString()).Find("quitBtn").GetComponent<Button_UI>().ClickFunc = () =>
                {
                    Application.Quit();
                };

        transform.Find(SubMenu.MainSub.ToString()).Find("howToPlayBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            ShowSub(SubMenu.howToPlaySub);
        };

        transform.Find(SubMenu.howToPlaySub.ToString()).Find("backBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            ShowSub(SubMenu.MainSub);
        };

        SetupButtonsSounds();
        ShowSub(SubMenu.MainSub);
    }

    private void ShowSub(SubMenu sub)
    {
        transform.Find(SubMenu.MainSub.ToString()).gameObject.SetActive(false);
        transform.Find(SubMenu.howToPlaySub.ToString()).gameObject.SetActive(false);
        transform.Find(sub.ToString()).gameObject.SetActive(true);

    }

    private void SetupButtonsSounds()
    {
        transform.Find(SubMenu.MainSub.ToString()).Find("playBtn").GetComponent<Button_UI>().AddButtonSounds();
        transform.Find(SubMenu.MainSub.ToString()).Find("quitBtn").GetComponent<Button_UI>().AddButtonSounds();
        transform.Find(SubMenu.MainSub.ToString()).Find("howToPlayBtn").GetComponent<Button_UI>().AddButtonSounds();
        transform.Find(SubMenu.howToPlaySub.ToString()).Find("backBtn").GetComponent<Button_UI>().AddButtonSounds();
    }
}
