using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonBehavior : MonoBehaviour
{
    public GameObject button;

    // When the Start Wave button is pressed
    public void OnButtonPress()
    {
        if (waves.waveActive == false)
        {
            waves.waveActive = true;
            button.SetActive(false);
            audioManager.instance.PlayTense();
        }
    }
}
