using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText, livesText;
    [SerializeField] private GameObject gameOverScreen;
    
    private static UIManager _instance;
    public static UIManager Instance 
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
            }

            return _instance;
        }
    }

    public void SetScore(int score)
    {
        scoreText.text = "Score = " + score;
    }

    public void SetLives(int lives)
    {
        if (lives < 0)
        {
            return;
        }

        livesText.text = "Lives = " + lives;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
