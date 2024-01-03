using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Purple_Wand_Script : MonoBehaviour
{
    [SerializeField]
    public GameObject Mana_Orb_Prefab;

    [SerializeField]
    public float Mana_Cost;

    [SerializeField]
    public float Mana_Orb_Speed;

    [SerializeField]
    public float Wand_Shoot_Delay;

    [SerializeField]
    public Player_Mana_Script Mana_Amount_Script;

    [HideInInspector]
    public GameObject Current_Orb;

    [HideInInspector]
    public float Next_Time_To_Shoot = 0f;

    public void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && Time.time > Next_Time_To_Shoot)
        {
            if (Mana_Amount_Script.Current_Mana_Amount >= Mana_Cost)
            {
                Shoot_Wand();
                Next_Time_To_Shoot += Wand_Shoot_Delay;
            }
        }
    }

    public void Shoot_Wand()
    {
        Ray Raycast = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit Hit;

        if (Physics.Raycast(Raycast, out Hit))
        {
            if (Current_Orb != null)
            {
                Destroy(Current_Orb);
            }

            Current_Orb = Instantiate(Mana_Orb_Prefab, Raycast.origin, Quaternion.identity);
            Rigidbody Orb_Rigid_Body = Current_Orb.GetComponent<Rigidbody>();
            Mana_Amount_Script.Take_Mana(Mana_Cost);

            if (Orb_Rigid_Body != null)
            {
                Orb_Rigid_Body.velocity = Raycast.direction * Mana_Orb_Speed;
            }
        }
    }

    public void Destroy_Orb()
    {
        Destroy(Current_Orb);
        Current_Orb = null;
    }

}
