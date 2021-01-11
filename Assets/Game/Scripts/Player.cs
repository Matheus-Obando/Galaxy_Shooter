using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public bool canTripleShot = false;
    public bool isSpeedBoostActive = false;
    public bool shieldActive = false;
    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;

    public int lives = 3;

    [SerializeField] private GameObject _explosionPrefab; // To select the explosion animation
    [SerializeField] private GameObject _laserPrefab; // To select the laser object
    [SerializeField] private GameObject _tripleShotPrefab; // To select the triple shot object
    [SerializeField] private GameObject _ShieldGameObject; // To select the shield object
	[SerializeField] private GameObject[] _engines = new GameObject[2];

    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private float _fireRate = 0.5f;
    private float _nextFire = 0.0f; // It will be used as a counter to the fire rate

    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
	private AudioSource _audioSource;

	private int hitCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (!_gameManager.isCoopMode)
        {
            transform.position = new Vector3(0, -2.1f, 0); // Start position when the scene start
        }

        if (_uiManager != null) // if _uiManager was founded 
        {
            _uiManager.UpdateLives(lives);
        }     

        //if (_spawnManager != null)
        //{
        //    _spawnManager.StartSpawnRoutines();
        //}

		hitCount = 0;
		_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move the ship on screen
        Movement();

        //Shoot laser
        if( isPlayerOne && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            ShootLaser();
        }

        else if (isPlayerTwo && (Input.GetKeyDown(KeyCode.Keypad0)))
        {
            ShootLaser();
        }
    }


    protected void Movement()
    {

        float horizontalInput;
        float verticalInput;
        // transform.Translate(Vector3.right);
        // transform.Translate(new Vector3(1,0,0)); //It has the same effect from above

        if (isPlayerOne)
        {
            horizontalInput = Input.GetAxis("Horizontal"); // Returns a negative or positive float value dependending on the input direction
            verticalInput = Input.GetAxis("Vertical"); // Returns a negative or positive float value dependending on the input direction

            // Movement methods
            if (isSpeedBoostActive) // On the alternative implementation this conditional section is unnecessary
            {
                transform.Translate(Vector3.right * Time.deltaTime * _moveSpeed * 2.0f * horizontalInput);
                transform.Translate(Vector3.up * Time.deltaTime * _moveSpeed * 2.0f * verticalInput);
            }

            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * _moveSpeed * horizontalInput); //It will move 1 meter per second instead of 60 meters per frame
                transform.Translate(Vector3.up * Time.deltaTime * _moveSpeed * verticalInput);
            }
        }

        if (isPlayerTwo)
        {
            if (isSpeedBoostActive)
            {
                if (Input.GetKey(KeyCode.Keypad8))
                {
                    transform.Translate(Vector3.up * Time.deltaTime * _moveSpeed * 2.0f);
                }

                if (Input.GetKey(KeyCode.Keypad2))
                {
                    transform.Translate(Vector3.down * Time.deltaTime * _moveSpeed * 2.0f);
                }

                if (Input.GetKey(KeyCode.Keypad4))
                {
                    transform.Translate(Vector3.left * Time.deltaTime * _moveSpeed * 2.0f);
                }

                if (Input.GetKey(KeyCode.Keypad6))
                {
                    transform.Translate(Vector3.right * Time.deltaTime * _moveSpeed * 2.0f);
                }
            }

            else
            {
                if (Input.GetKey(KeyCode.Keypad8))
                {
                    transform.Translate(Vector3.up * Time.deltaTime * _moveSpeed);
                }

                if (Input.GetKey(KeyCode.Keypad2))
                {
                    transform.Translate(Vector3.down * Time.deltaTime * _moveSpeed);
                }

                if (Input.GetKey(KeyCode.Keypad4))
                {
                    transform.Translate(Vector3.left * Time.deltaTime * _moveSpeed);
                }

                if (Input.GetKey(KeyCode.Keypad6))
                {
                    transform.Translate(Vector3.right * Time.deltaTime * _moveSpeed);
                }
            }

            // Limiters to prevent the ship from getting out of the screen (left, right, up, down) or in that case: horizontal [-8.3, 8.3] and vertical [-4.2, 0]
            if (transform.position.y > 0)
            {
                transform.position = new Vector3(transform.position.x, 0, 0); // If it hits the limit of the screen, it will always receive the limit position
            }
            else if (transform.position.y < -4.2f)
            {
                transform.position = new Vector3(transform.position.x, -4.2f, 0);  // Same
            }

            if (transform.position.x > 8.3f)
            {
                transform.position = new Vector3(8.3f, transform.position.y, 0);
            }
            else if (transform.position.x < -8.3f)
            {
                transform.position = new Vector3(-8.3f, transform.position.y, 0);
            }

        }
    }
    protected void ShootLaser()
    {
        if (Time.time > _nextFire)
        {
			_audioSource.Play();

            if (canTripleShot == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity); // Instantiate triple shot
            }

            else {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity); // Instantiate single shot
            }
            _nextFire = Time.time + _fireRate;
        }
        
    }

    public void Damage() // The damage logic is contabilized on the player
    {

        if(shieldActive)
        {
            shieldActive = false; // Deactivates shield, without receiving damage
            _ShieldGameObject.SetActive(false);
            return;
        }

		hitCount++;

		if (hitCount == 1)
		{
			// Turn Left Engine On
			_engines[0].SetActive(true);
		}

		else if (hitCount == 2)
		{
			// Turn Right Engine On
			_engines[1].SetActive(true);
		}

		else { 
            --lives; // --var operation is more fast than var-- operation (although both are correct)

			_uiManager.UpdateLives(lives);
            if(lives == 0){
                _uiManager.UpdateBestScore();
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                _gameManager.gameOver = true; // It will return to the title screen
                _uiManager.ShowTitleScreen(); // It will show the title screen again
                Destroy(this.gameObject); 
                // TODO: Ship destruction animation
            }
        }
    }

    public void SpeedBoostPowerupOn()
    {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());

        // Alternative implementation

        //this._moveSpeed += this._moveSpeed // It will double the move speed
        //StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;

        // Alternative implementation

        //yield return new WaitForSeconds(5.0f);
        //this._moveSpeed = this._moveSpeed/2.0f; // Turn the move speed back to the original value
    }

    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public void ShieldPowerupOn() // Shield does not have a cooldown logic, it will be deactivated based on damage
    {
        shieldActive = true;
        _ShieldGameObject.SetActive(true);
    }



}
