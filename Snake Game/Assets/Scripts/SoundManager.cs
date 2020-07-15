using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;

public enum Sounds
{
    SnakeMove,
    SnakeDie,
    SnakeEat,
    ButtonClick,
    ButtonOver
}

public static class SoundManager
{
    public static void PlaySound(Sounds sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
    }

    private static AudioClip GetAudioClip(Sounds sounds)
    {
        foreach(GameAssets.SoundAudioClip sounAudioClip in GameAssets.i.soundAudioClipsArray)
        {
            if(sounAudioClip.sound == sounds)
            {
                return sounAudioClip.audioClip;
            }
        }
        return null;
    }

    public static void AddButtonSounds(this Button_UI buttonUI)
    {
        buttonUI.MouseOverOnceFunc += () => SoundManager.PlaySound(Sounds.ButtonOver);
        buttonUI.ClickFunc += () => SoundManager.PlaySound(Sounds.ButtonClick);
    }
}
