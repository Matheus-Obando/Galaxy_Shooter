using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isCoopMode = false;
    public bool gameOver = true; // It will start as true

    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _coopPlayers;

    private UIManager _uiManager;
    private SpawnManager _spawnManager;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
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
                if (!isCoopMode)
                { 
                    Instantiate(_player, Vector3.zero, Quaternion.identity);
                }

                else
                {
                    Instantiate(_coopPlayers, Vector3.zero, Quaternion.identity);
                }

                gameOver = false;
                _uiManager.HideTitleScreen();
                _spawnManager.StartSpawnRoutines();
            }

            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
            }
        }
    }
}
