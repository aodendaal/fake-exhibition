using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool isTimerStarted = false;
    public static bool isGameOver = false;
    public static int score = 0;

    private float timeRemaining = 0;
    private float gameLength = 15f;

    public TMP_Text timeDisplay;
    public GameObject gameOverPanel;
    public GameObject gameStartPanel;

    public TMP_Text scoreText;
    public TMP_Text gameOverScoreText;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = gameLength;

        gameStartPanel.SetActive(true);
        gameOverPanel.SetActive(false);

        isTimerStarted = false;
        isGameOver = false;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTimerStarted)
        {
            if (Input.GetButtonDown("Player1 Fire1") || Input.GetButtonDown("Player1 Fire1"))
            {
                isTimerStarted = true;
                gameStartPanel.SetActive(false);

            }
        }

        if (isTimerStarted)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime();
            }
            else
            {
                GameOver();
            }
        }

        if (isGameOver)
        {
            if (Input.GetButtonDown("Player1 Fire1") || Input.GetButtonDown("Player1 Fire1"))
            {
                SceneManager.LoadScene(1);
            }
        }

        scoreText.text = score.ToString();
    }

    private void GameOver()
    {
        isTimerStarted = false;
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
