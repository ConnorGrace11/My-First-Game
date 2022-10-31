using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] notes;
    public GameObject enemy;
    public GameObject powerup;
    public GameObject player;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton;

    private float spawnRangeX = 18.0f;
    private float ySpawnPos = 0.5f;
    private float zSpawnPos = 10.0f;
    private float spawnIntervalNote = 1.0f;
    private float spawnIntervalEnemy = 3.0f;
    private int score = 0;
    private float time = 60.0f;

    public static bool isGameActive;
    //private float spawnIntervalPowerup = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            time -= Time.deltaTime;
            timeText.SetText("Timer: " + Mathf.Round(time));
        }
        if(time <= 0)
        {
            GameOver();
        }

    }

    IEnumerator SpawnEnemy()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnIntervalEnemy);
            float randomX = Random.Range(-spawnRangeX, spawnRangeX);
            Vector3 spawnPosEnemy = new Vector3(randomX, ySpawnPos, zSpawnPos);
            Instantiate(enemy, spawnPosEnemy, enemy.transform.rotation);

        }
    }
    IEnumerator SpawnRandomNote()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnIntervalNote);
            float randomX = Random.Range(-spawnRangeX, spawnRangeX);
            Vector3 spawnPosNote = new Vector3(randomX, ySpawnPos, zSpawnPos);
            int index = Random.Range(0, notes.Length);
            Instantiate(notes[index], spawnPosNote, notes[index].transform.rotation);

        }

    }

    public void StartGame(int difficulty)
    {
        spawnIntervalEnemy /= difficulty;
        spawnIntervalNote += difficulty;
        isGameActive = true;
        titleScreen.SetActive(false);
        StartCoroutine(SpawnRandomNote());
        StartCoroutine(SpawnEnemy());
    }

    public void UpdateScore(int updateScore)
    {
        if (isGameActive)
        {
            score += updateScore;
            scoreText.SetText("Score: " + score);
        }

    }
    public void UpdateHealth(int health)
    {
        healthText.SetText("Health: " + health);
    }

    public void UpdateAmmo(int ammo)
    {
        
        ammoText.SetText("Soundwaves: " + ammo);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
        Destroy(player);

    }
}
