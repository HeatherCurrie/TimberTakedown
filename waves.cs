using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml.Schema;

public class waves : MonoBehaviour
{
    public Transform leftSpawner;
    public Transform rightSpawner;
    public Transform plank;
    public Transform stump;
    public Transform tree;
    public Text waveText;
    public GameObject button;

    public health player;

    public static bool waveActive = false;
    public int currentWave = 0;
    public float damageMultiplier = 1f;

    int[][] numOfEnemy = new int[3][];

    private int counter = 0;
    public float spawnRate;
    public float timer = 0;
    private String chosenSpawner = "";
    private int[] totalEnemies = { 1, 3, 7, 10, 1, 3, 7, 10, 14, 10, 12, 15, 17, 21, 1};
    private int chosenEndlessWave = 11;

    // Start is called before the first frame update
    void Start()
    {
        // Set initial values
        audioManager.instance.PlayRelaxing();

        leftSpawner = transform.Find("EnemySpawnerLeft").transform;
        rightSpawner = transform.Find("EnemySpawnerRight").transform;
        button = GameObject.Find("WaveButton");

        waveText.text = "1";
        waveActive = false;
        damageMultiplier = 1f;

        // amount of each enemy on each wave 
        numOfEnemy[0] = new int[15] { 1, 3, 7, 10, 0, 0, 0, 0, 0, 5, 7, 10, 11, 13, 0};
        numOfEnemy[1] = new int[15] { 0, 0, 0, 0, 1, 3, 7, 10, 14, 5, 5, 5, 6, 8, 0};
        numOfEnemy[2] = new int[15] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 };
    }

    // Update is called once per frame
    void Update()
    {
        // Reset values if being replayed
        if ((scoreSingleton.instance.replay == true) && (SceneManager.GetActiveScene().buildIndex == 1))
        {
            ResetValues();
        }

        if (waveActive == true)
        {
            // If user beyond the hardcoded waves
            if (currentWave == 15)
            {
                Wave(chosenEndlessWave);

                // Check if wave has ended, this occurs when all enemies are destroyed, all of them have been spawned and the current scene is game
                if ((GameObject.Find("Plank(Clone)") == false) && (GameObject.Find("Stump(Clone)") == false) && (GameObject.Find("Tree(Clone)") == false) && (counter == totalEnemies[chosenEndlessWave]) && (SceneManager.GetActiveScene().buildIndex == 1))
                {
                    WaveEnded();

                    //Add damage to enemies
                    damageMultiplier = damageMultiplier * ChooseDamage();
                }
            } else
            {
                Wave(currentWave);

                // Check if wave has ended, this occurs when all enemies are destroyed, all of them have been spawned and the current scene is game
                if ((GameObject.Find("Plank(Clone)") == false) && (GameObject.Find("Stump(Clone)") == false) && (GameObject.Find("Tree(Clone)") == false) && (counter == totalEnemies[currentWave]) && (SceneManager.GetActiveScene().buildIndex == 1))
                {
                    WaveEnded();
                    currentWave += 1;
                }
            }
        }
    }

    // Goes through every enemy of the wave being played
    void Wave(int wave)
    {
        for (int j = 0; j < numOfEnemy.Length; j++)
        {
            for (int i = 0; i < numOfEnemy[j][wave]; i++)
            {
                // Timer between spawns
                if (timer < spawnRate)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    // Spawn enemy if not all enemies have spawned yet
                    if (counter < totalEnemies[wave])
                    {
                        spawnRate = ChooseRate();
                        Spawn(j);
                        timer = 0;
                        counter += 1;
                    }
                }
            }
        }
    }

    // set up for next wave
    void WaveEnded()
    {
        audioManager.instance.PlayRelaxing();

        waveActive = false;
        counter = 0;
        button.SetActive(true);

        player.GetComponent<health>().ResetHealth();

        AddScore();
        waveText.text = (scoreSingleton.instance.score + 1).ToString();
    }

    // Reset values on replay
    void ResetValues()
    {
        audioManager.instance.PlayRelaxing();

        leftSpawner = transform.Find("EnemySpawnerLeft").transform;
        rightSpawner = transform.Find("EnemySpawnerRight").transform;

        button = GameObject.Find("WaveButton");

        GameObject component = GameObject.Find("WaveDisplay");
        waveText = component.GetComponent<Text>();

        GameObject placeholder = GameObject.Find("Player");
        player = placeholder.GetComponent<health>();

        waveText.text = "1";
        waveActive = false;
        currentWave = 0;
        counter = 0;
        damageMultiplier = 1f;

        scoreSingleton.instance.SetReplay(false);
    }

    // Spawns enemy
    void Spawn(int type)
    {
        // randomly select left or right spawner
        chosenSpawner = ChooseSpawner();
        Transform enemy = null;

        // specify type of enemy
        if (type == 0)
        {
            enemy = plank;
        } else if (type == 1)
        {
            enemy = stump;
        } else
        {
            enemy = tree;
        }

        float yRange = 2.0f;
        float randomY = UnityEngine.Random.Range(leftSpawner.position.y - yRange, leftSpawner.position.y + yRange);

        // spawn enemy in selected spawner
        if (chosenSpawner == "leftSpawner")
        {
            Vector3 spawnPosition = new Vector2(leftSpawner.position.x, randomY);
            Instantiate(enemy, spawnPosition, transform.rotation);
        }
        else
        {
            Vector3 spawnPosition = new Vector2(rightSpawner.position.x, randomY);
            Instantiate(enemy, spawnPosition, transform.rotation);
        }
    }

    // Randomly decides which spawner the enemy spawns from
    String ChooseSpawner()
    {
        int num = UnityEngine.Random.Range(1, 3);

        if (num == 1)
        {
            return "leftSpawner";
        } else
        {
            return "rightSpawner";
        }
    }

    // Randomly choose the spawn rate
    float ChooseRate()
    {
        float num = UnityEngine.Random.Range(1, 50);
        return num;
    }

    // Chooses how much the enemies damage goes up by
    float ChooseDamage()
    {
        float num = UnityEngine.Random.Range(1f, 1.4f);
        return num;
    }

    // add to the singleton score when wave is complete
    void AddScore()
    {
        scoreSingleton.instance.score += 1;
    }
}
