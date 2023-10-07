using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class resources : MonoBehaviour
{
    public int wood = 0;
    public Text woodText;

    // Start is called before the first frame update
    void Start()
    {
        wood = 0;
        ChangeDisplay();
    }

    // Add wood
    public void AddResources(int amount)
    {
        wood += amount;
        ChangeDisplay();
    }

    // Remove wood
    public void RemoveResources(int amount) 
    {
        wood -= amount;
        ChangeDisplay();
    }

    // Returns amount of wood the user has
    public int FindResources()
    {
        return wood;
    }

    // Changes the wood display
    void ChangeDisplay()
    {
        woodText.text = wood.ToString() + " Wood";
    }
}
