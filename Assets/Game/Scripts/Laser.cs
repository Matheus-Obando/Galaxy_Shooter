using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField] private float _speed = 10.0f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if (transform.position.y > 6.0f){

            if (transform.parent != null) // It means that these laser is a child (triple laser)
            {
                Destroy(transform.parent.gameObject); // It will destroy the triple lasers parent (laser)
            }
            Destroy(this.gameObject);
        }
    }


    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {

       if(collision.tag == "Enemy")
        {
            //EnemyAI enemy = collision.GetComponent<EnemyAI>();
            if (transform.parent != null)
            {
                Destroy(transform.parent);
            }
            Destroy(this.gameObject);
            //Destroy(enemy);
            Destroy(collision.GetComponent<EnemyAI>().gameObject);
        }
    }
    */
}
