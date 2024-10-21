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
    public Button restartButton;
    public bool isGameActive;

    private int score;

    void Start()
    {
        gameOverText.gameObject.SetActive(false);

        gameOverCompletedText.gameObject.SetActive(false);
        
        restartButton.gameObject.SetActive(false);
        
        restartButton.onClick.AddListener(RestartGame);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;

        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);

        isGameActive = false;
        
        restartButton.gameObject.SetActive(true);
    }

    public void ShowGameCompletedText()
    {
        gameOverCompletedText.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}