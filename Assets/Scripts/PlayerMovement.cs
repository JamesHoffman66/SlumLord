using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;



public class PlayerMovement : MonoBehaviour
{

    public float horizontalInput;
    public float verticalInput;
    private float speed = 5f;
    private Rigidbody rb;
    public float xRange = 19f;
    public GameObject Swing;
    public bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Swing.gameObject.SetActive(false);
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Player movement using wasd to move horizontally and vertically 
        horizontalInput = Input.GetAxis("Horizontal");
                

        verticalInput = Input.GetAxis("Vertical");
                

        // Player cant go through rigid bodies
        Vector3 moveVector = new Vector3(horizontalInput, verticalInput, 0);
        rb.velocity = (moveVector * speed);

        // Player movement boundaries 
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if(transform.position.y < 1.3f)
        {
            transform.position = new Vector3(transform.position.x, 1.3f, transform.position.z);
        }
        if(transform.position.y > 19.4f)
        {
            transform.position = new Vector3(transform.position.x, 19.4f, transform.position.z);
        }

        if (!attacking)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Swing.gameObject.SetActive(true);
                StartCoroutine(attackDelay(0.2f));

            }
        }
    }

    IEnumerator attackDelay(float delay)
    {
        attacking = true;
        yield return new WaitForSeconds(delay);
        Swing.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        attacking = false;
    }
}
