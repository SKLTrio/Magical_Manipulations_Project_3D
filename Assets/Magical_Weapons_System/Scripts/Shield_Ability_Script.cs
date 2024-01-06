using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shield_Ability_Script : MonoBehaviour
{
    [SerializeField]
    public GameObject Shield_Placed_Prefab;

    [SerializeField]
    public float Shield_Cooldown_Period;

    [SerializeField]
    public float Spawn_Distance;

    [SerializeField]
    public float Mana_Cost;

    [SerializeField]
    public GameObject Player_Object;

    [SerializeField]
    public Menu_Controller Menu_Controller_Script;

    [HideInInspector]
    Player_Mana_Script Mana_Amount_Script;

    [HideInInspector]
    public float Last_Shield_Placed = 0f;

    private void Start()
    {
        Mana_Amount_Script = Player_Object.GetComponent<Player_Mana_Script>();
    }

    public void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame && Time.time >= Last_Shield_Placed)
        {
            if (Mana_Amount_Script.Current_Mana_Amount >= Mana_Cost)
            {
                if (!Menu_Controller_Script.Is_Game_Paused)
                {
                    if (!Menu_Controller_Script.Is_Game_Done)
                    {
                        Spawn_Shield();
                        Last_Shield_Placed += Shield_Cooldown_Period;
                    }
                }
            }
        }
    }

    public void Spawn_Shield()
    {
        Vector3 Shield_Spawn_Position = Player_Object.transform.position + Player_Object.transform.forward * Spawn_Distance;

        Quaternion Shield_Spawn_Rotation = Player_Object.transform.rotation;

        Shield_Spawn_Rotation *= Quaternion.Euler(0, -180, 0);

        Shield_Spawn_Position.y = 0;

        GameObject Spawned_Shield = Instantiate(Shield_Placed_Prefab, Shield_Spawn_Position, Shield_Spawn_Rotation);

        Last_Shield_Placed = Time.time;
    }
}
