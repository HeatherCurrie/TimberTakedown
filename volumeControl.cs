using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class volumeControl : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider effectsSlider;

    float musicVolume; 
    float effectVolume;

    // Start is called before the first frame update
    void Start()
    {
        SetMusicVol();
        SetEffectsVol();
    }

    // Sets the music value depending on slider value
    public void SetMusicVol() 
    {
        musicVolume = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(musicVolume)*20);
    }

    // Sets the sound effects value depending on slider value
    public void SetEffectsVol()
    {
        effectVolume = effectsSlider.value;
        audioMixer.SetFloat("Effect", Mathf.Log10(effectVolume) * 20);
    }
}
