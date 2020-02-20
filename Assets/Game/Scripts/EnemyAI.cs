using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    // Explosion animation
    [SerializeField] private GameObject _enemyExplosionPrefab;
    // Variable for your speed
    [SerializeField] private float _moveSpeed;
    // Create a handle and reference to the UIManager class
    private UIManager _uiManager;
	// Create a handle and reference to the AudioSource class
	private AudioSource _audioSource;
	// Explosion audio clip
	[SerializeField]
	private AudioClip _clip;

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(0, 2.1f, 0);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move down
        transform.Translate(Vector3.down * Time.deltaTime * _moveSpeed);

        // When of the screen on the bottom
        // Respawn back on the top with a new x position between the bounds of the screen
        if (transform.position.y < -7.0f)
        {
            transform.position = new Vector3(Random.Range(-7.8f, 7.8f), 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {

            Player player = collision.GetComponent<Player>();
            player.Damage();
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity); // Transform position == current position
			AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
			Destroy(this.gameObject);
        }

        else if (collision.tag == "Laser") // Trigger the destroy sequence by itself
        {
            if (collision.transform.parent != null)
            {
                Destroy(collision.transform.parent.gameObject);
            }

			Destroy(collision.gameObject);
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity); // Transform position == current position
			_uiManager.UpdateScore();
			AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
        }
    }

}
