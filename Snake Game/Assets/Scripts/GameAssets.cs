using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets i;
    public Sprite snakeHeadSprites;
    public Sprite foodSprite;
    public Sprite snakeBodySprite;

    //public AudioClip snakeMove;
    //public AudioClip snakeDie;
    //public AudioClip snakeEat;
    //public AudioClip buttonClick;
    //public AudioClip buttonOver;

    public SoundAudioClip[] soundAudioClipsArray;

    private void Awake()
    {
        i = this;
    }

    [Serializable]
    public class SoundAudioClip
    {
        public Sounds sound;
        public AudioClip audioClip;
    }
}
