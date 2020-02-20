using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true; // It will start as true
    public GameObject player;

    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // If game over is true
    // If space key is pressed
    // Spawn the player
    // Game over is false
    // Hide the title screen

    private void Update()
    {
        if (gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(player, Vector3.zero, Quaternion.identity);
                gameOver = false;
                _uiManager.HideTitleScreen();
            }
        }
    }
}
