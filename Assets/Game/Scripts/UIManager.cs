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
    public GameObject mainMenu;

    public int score;
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
