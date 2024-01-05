using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana_Consumable_Script : MonoBehaviour
{
    [SerializeField]
    public float Mana_Give_Amount;

    [SerializeField]    
    public string Mana_Colour;

    private void OnTriggerEnter(Collider Collider)
    {
        if (Collider.CompareTag("Player"))
        {
            if (Mana_Colour == "Blue")
            {
                Destroy(gameObject);
                Player_Mana_Script Mana_Script = Collider.GetComponent<Player_Mana_Script>();
                Mana_Script.Add_Mana(Mana_Give_Amount);
            }
            else
            {
                Destroy(gameObject);
                Player_Mana_Script Mana_Script = Collider.GetComponent<Player_Mana_Script>();
                Mana_Script.Add_Golden_Mana(Mana_Give_Amount);
            }
        }
    }
}
