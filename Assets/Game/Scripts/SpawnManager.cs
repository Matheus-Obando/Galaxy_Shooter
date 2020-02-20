using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyShipPrefab;
    [SerializeField] private GameObject[] powerups; // There is 3 types of powerup

    //[SerializeField] private float enemySpawnRate;
    //[SerializeField] private float powerupSpawnRate;
    // Start is called before the first frame update
    private GameManager _gameManager;
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void StartSpawnRoutines()
    {
        StartCoroutine(SpawnEnemyOnGameRoutine());
        StartCoroutine(SpawnPowerupOnGameRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Create a coroutine to spawn the enemy on every 5 seconds

    public IEnumerator SpawnEnemyOnGameRoutine()
    {
        while (true)
        {
            Instantiate(enemyShipPrefab, new Vector3(Random.Range(-7.8f, 7.8f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    public IEnumerator SpawnPowerupOnGameRoutine()
    {
        while (true)
        {
            int randomPowerup = Random.Range(0, 3); // '3' because is a closed interval on the right side
            Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-7.8f, 7.8f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(8.0f);
        }
    }
}