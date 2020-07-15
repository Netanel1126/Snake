using System.Collections;
using System.Collections.Generic;
using CodeMonkey;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    private Snake snake;
    private LevelGrid levelGrid;

    private static GameHandler sharedInstance;
    private bool isPaused = false;

    private void Awake()
    {
        sharedInstance = this;
        Score.InitializeStatic();
    }

    void Start()
    {
        levelGrid = new LevelGrid(20, 20);
        snake.Setup(levelGrid);
        levelGrid.Setup(snake);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                GameHandler.ResumeGame();
            }
            else
            {
                GameHandler.PauseGame();
            }
        }
    }

    public static void SnakeDied()
    {
        GameOverWindow.ShowStatic(Score.IsNewHighScore);
        ScoreWindow.HideStatic();
    }

    public static void ResumeGame()
    {
        sharedInstance.isPaused = false;
        PauseWindow.HideStatic();
        Time.timeScale = 1f;
    }

    public static void PauseGame()
    {
        sharedInstance.isPaused = true;
        PauseWindow.ShowStatic();
        Time.timeScale = 0f;
    }
}
