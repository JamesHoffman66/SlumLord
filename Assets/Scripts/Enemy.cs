using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 0.1f;
    private Rigidbody enemyRB;
    private GameObject player;


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
}
