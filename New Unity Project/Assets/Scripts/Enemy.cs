using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;
    private GameManager gameManager;
    private AudioSource enemyAudio;

    public int speed = 20;
    public float bumpSpeed = 100.0f;
    private float xRange = 18;

    public AudioClip hurtSound;
    public AudioClip boomSound;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        enemyAudio = GetComponent<AudioSource>();
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
        ConstrainEnemy();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(GameManager.isGameActive)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                enemyAudio.PlayOneShot(hurtSound);
                PlayerController.healthBar--;
                gameManager.UpdateHealth(PlayerController.healthBar);
                Debug.Log("Health decreased to " + PlayerController.healthBar + " ... Ouch!");
                if (PlayerController.healthBar <= 0)
                {
                    gameManager.GameOver();
                    gameManager.UpdateHealth(0);
                }
                Vector3 bumpDirection = (transform.position - player.transform.position).normalized;
                enemyRb.AddForce(bumpDirection * bumpSpeed, ForceMode.Impulse);

            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("projectile"))
        {
            PlayerController.playerAudio.PlayOneShot(boomSound);
            Destroy(gameObject);
            Destroy(other.gameObject);
            gameManager.UpdateScore(20);
            Debug.Log("Huge Harmony!");
        }
    }
    private void MoveEnemy()
    {
        
        enemyRb.AddForce(Vector3.forward * -speed * Time.deltaTime);
        if(transform.position.z < -15)
        {
            Destroy(gameObject);
        }
    }

    private void ConstrainEnemy()
    {
        /*if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
        else if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }*/
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
    }
}
