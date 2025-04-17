using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 0.1f;
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

    // Makes it so when the enemy runs into the player, the player will get bounced back
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromEnemy = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Collided with: " + collision.gameObject.name);
            playerRigidbody.AddForce(awayFromEnemy * knockBackStrength, ForceMode.Impulse);
        }
    }
}
