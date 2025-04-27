using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{

    public float horizontalInput;
    public float verticalInput;
    private float speed = 5f;
    private Rigidbody rb;
    public float xRange = 19f;
    public GameObject Swing;
    public GameObject SwingLeft;
    public bool attacking;
    public bool facingLeft;
    private bool hasPowerUp;
    public TextMeshProUGUI livesText;
    private int lives = 4;
    public TextMeshProUGUI GameOverText;
    public UnityEngine.UI.Button RestartButton;
    public bool isGameActive;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Swing.gameObject.SetActive(false);
        attacking = false;

        GameOverText.gameObject.SetActive(false);

        UpdateLives();

    }

    // Update is called once per frame
    void Update()
    {
        // Player movement using wasd to move horizontally and vertically 
        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0)
        {
            facingLeft = false;
            // spriteRenderer.flipX = false;
        }

        if (horizontalInput < 0)
        {
            facingLeft = true;
            // spriteRenderer.flipX = true;
        }


        verticalInput = Input.GetAxis("Vertical");
                

        // Player cant go through rigid bodies
        Vector3 moveVector = new Vector3(horizontalInput, verticalInput, 0);
        rb.velocity = (moveVector * speed);

        // Player movement boundaries 
        if (transform.position.x < -19.3f)
        {
            transform.position = new Vector3(-19.3f, transform.position.y, transform.position.z);
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

        // attack code (destroys enemy when player attacks)
        if (!attacking)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // directional attacking
                if (facingLeft)
                {
                    SwingLeft.gameObject.SetActive(true);
                }
                else 
                { 
                    Swing.gameObject.SetActive(true); 
                }
                
                StartCoroutine(attackDelay(0.2f));

            }
        }
    }

    public void UpdateLives()
    {
        lives--;
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        GameOverText.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
        isGameActive = false;
    }




    // when the player collides with the power-Up, the power-up gets destroyed and logs it to the console
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Power-Up"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            Debug.Log("Power-Up destroyed");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasPowerUp)
        {

        }
    }



    // delays the swing 
    IEnumerator attackDelay(float delay)
    {
        attacking = true;
        yield return new WaitForSeconds(delay);
        Swing.gameObject.SetActive(false);
        SwingLeft.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        attacking = false;
    }
}
