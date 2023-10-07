using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class towerMenu : MonoBehaviour
{
    public turretBuilding turretBuilding;
    private resources resources;

    private int standardCost = 150;
    private int rangedCost = 200;
    private int powerfulCost = 500;

    public Text standardError;
    public Text rangedError;
    public Text powerfulError;
    public Text placeTower;

    // Start is called before the first frame update
    void Start()
    {
        resources = GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<resources>();
        this.gameObject.SetActive(false);
        turretBuilding = turretBuilding.instance;
    }

    // When buy turret button is clicked
    public void toggleActivity()
    {
        if (this.gameObject.activeSelf)
        {
            // Set values to default when closed
            this.gameObject.SetActive(false);
            standardError.gameObject.SetActive(false);
            rangedError.gameObject.SetActive(false);
            powerfulError.gameObject.SetActive(false);
        } else
        {
            this.gameObject.SetActive(true);
        }
    }

    // When player clicks standard turret buy button
    public void BuyStandardTurret()
    {
        if (resources.FindResources() >= standardCost) 
        {
            audioManager.instance.PlayEffect(audioManager.instance.purchase);
            turretBuilding.SetChosenTurret(turretBuilding.standardTurret);
            resources.RemoveResources(standardCost);
            placeTower.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        } else
        {
            standardError.gameObject.SetActive(true);
            audioManager.instance.PlayEffect(audioManager.instance.error);
        }
    }

    // When player clicks ranged turret buy button
    public void BuyRangedTurret()
    {
        if (resources.FindResources() >= rangedCost)
        {
            audioManager.instance.PlayEffect(audioManager.instance.purchase);
            turretBuilding.SetChosenTurret(turretBuilding.rangedTurret);
            resources.RemoveResources(rangedCost);
            placeTower.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else
        {
            rangedError.gameObject.SetActive(true);
            audioManager.instance.PlayEffect(audioManager.instance.error);
        }
    }

    // When player clicks powerful turret buy button
    public void BuyPowerfulTurret()
    {
        if (resources.FindResources() >= powerfulCost)
        {
            audioManager.instance.PlayEffect(audioManager.instance.purchase);
            turretBuilding.SetChosenTurret(turretBuilding.powerfulTurret);
            resources.RemoveResources(powerfulCost);
            placeTower.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else
        {
            powerfulError.gameObject.SetActive(true);
            audioManager.instance.PlayEffect(audioManager.instance.error);
        }
    }
}
