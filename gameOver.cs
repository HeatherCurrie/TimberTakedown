using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class gameOver : MonoBehaviour
{
    public Text wavesText;

    private void Start()
    {
        audioManager.instance.PlayRelaxing();
        DisplayWaves();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    void DisplayWaves()
    {
        wavesText.text = scoreSingleton.instance.score.ToString();
    }
}
