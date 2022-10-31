using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -20 || transform.position.z > 15)
        {
            if (gameObject.CompareTag("Electric"))
            {
                gameManager.UpdateScore(-5);
            }else if (gameObject.CompareTag("Bass"))
            {
                gameManager.UpdateScore(-10);
            }else if (gameObject.CompareTag("Acoustic"))
            {
                gameManager.UpdateScore(-15);
            }
            Destroy(gameObject);
        }
    }
}
