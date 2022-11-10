using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static AudioSource playerAudio;
    public float speed;
    private int gunCapacity = 0;
    public static int healthBar = 3;
    private float zRange = -5;
    private float xRange = 18;

    public AudioClip grabSound;
    public AudioClip fireSound;
    public AudioClip powerupSound;
    public ParticleSystem guitarParticle;
    public ParticleSystem powerupParticle;
    public ParticleSystem hurtParticle;
    public GameObject projectilePrefab;
    private GameManager gameManager;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void  FixedUpdate()
    {
        if (GameManager.isGameActive)
        {
            animator.SetFloat("Speed_f", 5);

            //Abstraction
            MovePlayer();
            ConstrainPlayer();
            FireProjectile();
        }

        
    }

    //get arrow key input to move player
    void MovePlayer()
    {

        
        float horizontalInput;


        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
    }

    //Constrain players within game area
    void ConstrainPlayer()
    {       
        if (transform.position.z != zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
        /*else if (transform.position.z < zRange - 1)
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

    private void OnCollisionEnter(Collision collision)
    {
        if(GameManager.isGameActive)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                gameManager.UpdateScore(-20);
                hurtParticle.Play();
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(GameManager.isGameActive)
        {
            if (other.gameObject.CompareTag("Acoustic"))
            {
                gunCapacity += 3;
                gameManager.UpdateAmmo(gunCapacity);
                gameManager.UpdateScore(15);
                guitarParticle.Play();
                Debug.Log("Gun capacity increased to " + gunCapacity);
                Destroy(other.gameObject);
                playerAudio.PlayOneShot(grabSound);

            }
            else if (other.gameObject.CompareTag("Bass"))
            {
                gunCapacity += 2;
                gameManager.UpdateAmmo(gunCapacity);
                gameManager.UpdateScore(10);
                guitarParticle.Play();
                Debug.Log("Gun capacity increased to " + gunCapacity);
                Destroy(other.gameObject);
                playerAudio.PlayOneShot(grabSound);

            }
            else if (other.gameObject.CompareTag("Electric"))
            {
                gunCapacity++;
                gameManager.UpdateAmmo(gunCapacity);
                gameManager.UpdateScore(5);
                guitarParticle.Play();
                Debug.Log("Gun capacity increased to " + gunCapacity);
                Destroy(other.gameObject);
                playerAudio.PlayOneShot(grabSound);

            }
            else if (other.gameObject.CompareTag("Powerup"))
            {
                playerAudio.PlayOneShot(powerupSound);
                powerupParticle.Play();
                healthBar++;
                gameManager.UpdateHealth(healthBar);
                Debug.Log("Health increased to " + healthBar);
                Destroy(other.gameObject);
            }
        }
        
        
        
    }
    void FireProjectile()
    {
        if (gunCapacity > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerAudio.PlayOneShot(fireSound);
                Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), projectilePrefab.transform.rotation);
                gunCapacity--;
                gameManager.UpdateAmmo(gunCapacity);
                Debug.Log("Gun capacity decreased to " + gunCapacity);
            }

        }
    }

}
