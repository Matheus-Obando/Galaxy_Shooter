using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImageDisplay;
    public Text scoreText;
    public Text bestScoreText;
    public GameObject mainMenu;

    public int score;
    public int bestScore = 0;

    public void Start()
    {
        bestScore = PlayerPrefs.GetInt("HighScore");
        bestScoreText.text = string.Format("Best: {0}", bestScore);
    }

    public void UpdateLives(int currentLives)
    {
        Debug.Log("Player lives: " + currentLives);
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = string.Format("Score: {0}", score);
    }

    public void UpdateBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("HighScore", bestScore);
            bestScoreText.text = string.Format("Best: {0}", bestScore);
        }
    }

    public void ShowTitleScreen()
    {
  
        mainMenu.SetActive(true);
        score = 0;
    }

    public void HideTitleScreen()
    {
        mainMenu.SetActive(false);
        scoreText.text = "Score: ";
    }

    public void ResumeGame()
    {

        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.ResumeGame();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
    }
}
