using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class plot : MonoBehaviour
{
    private GameObject turret;
    public turretBuilding turretBuilding;
    public Text placeTower;
    private bool empty;

    // Start is called before the first frame update
    void Start()
    {
        turretBuilding = turretBuilding.instance;
        empty = true;
    }

    void OnMouseOver()
    {
        // if plot clicked
        if (Input.GetMouseButtonDown(0))
        {
            if (turretBuilding.getChosenTurret() == null)
            {
                return;
            }

            // If plot empty
            if (empty == true)
            {
                // Creates chosen turret
                audioManager.instance.PlayEffect(audioManager.instance.thud);
                GameObject chosenTurret = turretBuilding.getChosenTurret();
                turret = (GameObject)Instantiate(chosenTurret, transform.position, transform.rotation);
                placeTower.gameObject.SetActive(false);
                turretBuilding.SetChosenTurret(null);
                empty = false;  
            }
        }
    }
}
