using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 50f;
    private Rigidbody enemyRB;
    private GameObject player;
    private float knockBackStrength = 80f;


    // Start is called before the first frame update
    void Start()
    {
        // Initializes enemyRB and player
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy follows player
        enemyRB.AddForce((player.transform.position - transform.position).normalized * speed);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Swing"))
        {
            Debug.Log("Enemy KO");
            Destroy(gameObject);
        }

    }

    // Makes it so when the enemy runs into the player, the player will get bounced back
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromEnemy = (collision.gameObject.transform.position - transform.position);

            // Writes to the console that the enemy made contact with the player
            Debug.Log("Collided with: " + collision.gameObject.name);

            // Knocks back the player
            playerRigidbody.AddForce(awayFromEnemy * knockBackStrength, ForceMode.Impulse);

        }
    }

    
}
