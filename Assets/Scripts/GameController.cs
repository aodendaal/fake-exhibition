using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool isStarted = false;
    public static bool isGameOver = false;
    public static bool isPaused = false;

    public static int score = 0;

    private float timeRemaining = 0;
    private float gameLength = 20f;

    [Header("Panels")]
    public GameObject gameOverPanel;
    public GameObject gameStartPanel;
    public GameObject gamePausePanel;

    [Header("Display Text")]
    public TMP_Text timeDisplay;
    public TMP_Text scoreText;
    public TMP_Text gameOverScoreText;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = gameLength;

        gameStartPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gamePausePanel.SetActive(false);

        isStarted = false;
        isGameOver = false;
        isPaused = false;

        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStarted)
        {
            if (Input.GetButtonDown("Player1 Fire1") || Input.GetButtonDown("Player2 Fire1"))
            {
                isStarted = true;
                gameStartPanel.SetActive(false);

            }
        }

        if (isStarted && !isPaused)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime();

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Pause();
                }
            }
            else
            {
                GameOver();
            }
        }
        else if (isStarted && isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Unpause();
            }

            if (Input.GetButtonDown("Player1 Fire1") || Input.GetButtonDown("Player2 Fire1"))
            {
                Application.Quit();
            }
        }

        if (isGameOver)
        {
            if (Input.GetButtonDown("Player1 Fire1") || Input.GetButtonDown("Player2 Fire1"))
            {
                SceneManager.LoadScene(1);
            }
        }

        scoreText.text = score.ToString();
    }

    private void Unpause()
    {
        gamePausePanel.SetActive(false);
        isPaused = false;
    }

    private void Pause()
    {
        gamePausePanel.SetActive(true);
        isPaused = true;
    }

    private void GameOver()
    {
        isStarted = false;
        isGameOver = true;
        gameOverPanel.SetActive(true);
        gameOverScoreText.text = score.ToString();
    }

    private void DisplayTime()
    {
        var minutes = Mathf.FloorToInt(timeRemaining / 60f);
        var seconds = Mathf.FloorToInt(timeRemaining - (minutes * 60));

        timeDisplay.text = $"{minutes}:{seconds:00}";

        if (minutes == 0 && seconds < 21)
        {
            timeDisplay.color = Color.red;
        }

    }
}
