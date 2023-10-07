using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretBuilding : MonoBehaviour
{
    public static turretBuilding instance;

    private GameObject chosenTurret;
    public GameObject standardTurret;
    public GameObject rangedTurret;
    public GameObject powerfulTurret;

    // Ensures theres only 1 instance
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Returns the turret chosen by user
    public GameObject getChosenTurret ()
    {
        return chosenTurret;
    }

    // Sets turret chosen by user
    public void SetChosenTurret(GameObject turret)
    {
        chosenTurret = turret;
    }
}
