using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{
    public Text highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        audioManager.instance.PlayRelaxing();

        // If it is not the first time loading the main menu
        if (scoreSingleton.instance != null)
        {
            scoreSingleton.instance.SetHighScore();
            HighScoreDisplay();
            scoreSingleton.instance.SetReplay(true);
        }
    }

    // When start game button is pressed
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    // When exit game button is pressed
    public void ExitGame()
    {
        Application.Quit();
    }

    // Update high score display
    void HighScoreDisplay()
    {
        highScoreText.text = "High Score - Wave " + scoreSingleton.instance.highScore;
    }
}
