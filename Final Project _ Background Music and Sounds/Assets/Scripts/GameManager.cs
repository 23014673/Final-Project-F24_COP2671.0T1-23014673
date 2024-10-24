using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI gameOverCompletedText;
    public TextMeshProUGUI timerText;
    public GameObject gameOverBackground;
    public GameObject gameCompletedBackground;
    public Button restartButton;
    public Button pauseButton;                  // GAME PAUSE BUTTON
    public bool isGameActive;
    private int score;
    private float timer = 30f;
    private bool isPaused = false;              // GAME IS PAUSED BUTTON
    
    void Start()
    {
        gameOverText.gameObject.SetActive(false);

        gameOverCompletedText.gameObject.SetActive(false);

        gameOverBackground.SetActive(false);

        gameCompletedBackground.SetActive(false);

        restartButton.gameObject.SetActive(false);
        
        restartButton.onClick.AddListener(RestartGame);
        
        StartTimer();
    }

    void Update()
    {
        if (isGameActive)
        {
            UpdateTimer();
        }
    }

    void StartTimer()
    {
        isGameActive = true;

        timer = 30f;
    }

    void UpdateTimer()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer = 0;

            GameOver();
        }

        int minutes = Mathf.FloorToInt(timer / 60);

        int seconds = Mathf.FloorToInt(timer % 60);

        timerText.text = "Timer - " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        
        scoreText.text = "Score -  " + score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);

        gameOverBackground.SetActive(true);

        isGameActive = false;

        restartButton.gameObject.SetActive(true);
    }

    public void ShowGameCompletedText()
    {
        gameOverCompletedText.gameObject.SetActive(true);

        gameCompletedBackground.SetActive(true);

        isGameActive = false;

        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}