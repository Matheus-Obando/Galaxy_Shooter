using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

	[SerializeField]
	private float _speed = 3.0f;
	[SerializeField]
	private int PowerUpId; // 0 = Triple Shot, 1 = Speed Boost, 2 = Shield Boost
	[SerializeField]
	private AudioClip _clip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y < -7.0f)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered colider with: " + collision.name);

        if (collision.tag == "Player") // It will be used to make sure that only the player will receive the power-up (a enemie can not have a power-up boost)
        {
            // Steps to be implemented:
            Player player = collision.GetComponent<Player>(); // Access the player script

            if (player != null) // It verifies if player does exists after the GetComponent execution
            {
                // Enable triple shot
                if (PowerUpId == 0)
                {
                    player.TripleShotPowerupOn();
                }
                // Enable speed boost
                else if (PowerUpId == 1)
                {
                    player.SpeedBoostPowerupOn();
                }

                // Enable shield boost
                else if (PowerUpId == 2)
                {
                    player.ShieldPowerupOn(); //TODO: Shield Boost method
                }
            }

			AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject); // Destroy the powerup after this
        }
    }
}
