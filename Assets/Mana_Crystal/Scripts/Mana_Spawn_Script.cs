using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana_Spawn_Script : MonoBehaviour
{
    [SerializeField]
    public GameObject Mana_Consumable_Prefab;

    [SerializeField]
    public Mana_Crystal_Health_Script Crystal_Health_Script;

    public void Update()
    {
        if (Crystal_Health_Script.Current_Health <= 100 && Crystal_Health_Script.Current_Health >= 85)
        {
            //Debug.Log("Mana will spawn every 5 to 10 seconds");
        }

        else if (Crystal_Health_Script.Current_Health < 85 && Crystal_Health_Script.Current_Health >= 50)
        {
            //Debug.Log("Mana will spawn every 10 to 20 seconds");
        }

        else if (Crystal_Health_Script.Current_Health < 50 && Crystal_Health_Script.Current_Health >= 25)
        {
            //Debug.Log("Mana will spawn every 20 to 40 seconds");
        }

        else if (Crystal_Health_Script.Current_Health < 25)
        {
            //Debug.Log("No more mana will spawn");
        }
    }
}
