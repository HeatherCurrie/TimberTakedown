using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static audioManager instance;

    public AudioSource effects;

    public AudioSource relaxing;
    public AudioSource tense;

    public AudioClip error;
    public AudioClip purchase;
    public AudioClip bullet;
    public AudioClip ouch;
    public AudioClip damage;
    public AudioClip thud;

    // Ensures theres only 1 instance that moves between scenes
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

    // Play sound effect
    public void PlayEffect(AudioClip clip)
    {
        effects.PlayOneShot(clip);
    }
    
    // Play relaxing music
    public void PlayRelaxing()
    {
        tense.Stop();
        relaxing.Play();
    }

    // Play tense music
    public void PlayTense()
    {
        relaxing.Stop();
        tense.Play();
    }
}
