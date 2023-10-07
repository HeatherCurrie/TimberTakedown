using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreSingleton : MonoBehaviour
{
    public static scoreSingleton instance;
    public int score;
    public int highScore = 0;
    public bool replay = false;

    // Ensures theres only 1 instance that stays between scenes
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Sets the users high score
    public void SetHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
        }
    }

    // Resets score on replay
    public void SetReplay(bool value)
    {
        replay = value;

        // Reset score
        if (value == true)
        {
            score = 0;
        }
    }
}
