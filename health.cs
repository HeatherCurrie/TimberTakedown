using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    public float playerHealth = 100;
    public int numOfHearts = 4;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // update is called once per frame
    void Update()
    {
        int roundedHealth = FindRoundedHealth();

        // For the amount of hearts
        for (int i = 0; i < hearts.Length; i++)
        {
            // Displays hearts as full or empty
            if (i < roundedHealth)
            {
                hearts[i].sprite = fullHeart; 
            } else
            {
                hearts[i].sprite = emptyHeart;
            }

            // Display correct amount of hearts
            if (i < numOfHearts) 
            {
                hearts[i].enabled = true;
            } else
            {
                hearts[i].enabled = false;
            }
        }
    }

    // Find amount of hearts which should be displayed, depending on the characters health
    int FindRoundedHealth()
    {
        if (playerHealth > 80)
        {
            return 5;
        } else if (playerHealth <= 80 && playerHealth > 60)
        {
            return 4;
        } else if (playerHealth <= 60 && playerHealth > 40)
        {
            return 3;
        } else if (playerHealth <= 40 && playerHealth > 20)
        { 
            return 2;
        } else if (playerHealth <= 20 && playerHealth > 0)
        {
            return 1;
        } else
        {
            return 0;
        }
    }

    // Player gets attacked
    public void TakeDamage(float damage)
    {
        playerHealth = playerHealth - damage;

        // Particle effect
        GetComponent<ParticleSystem>().Play();
        ParticleSystem.EmissionModule emmit = GetComponent<ParticleSystem>().emission;
        emmit.enabled = true;

        audioManager.instance.PlayEffect(audioManager.instance.ouch);

        // Die when out of health
        if (playerHealth <= 0)
        {
            Die();
        }
    }

    // Set health to max
    public void ResetHealth()
    {
        playerHealth = 100;
    }

    // Ends the game
    void Die()
    {
        // Set hearts to empty
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = emptyHeart;
        }

        // Disables the collider
        GetComponent<Collider2D>().enabled = false;

        // Disables the script
        this.enabled = false;

        // Change to game over scene
        Destroy(gameObject);
        SceneManager.LoadScene(2);
    }
}
