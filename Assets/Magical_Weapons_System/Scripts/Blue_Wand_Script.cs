using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Blue_Wand_Script : MonoBehaviour
{
    [SerializeField]
    public GameObject Mana_Orb_Prefab;

    [SerializeField]
    public float Mana_Orb_Speed;

    [HideInInspector]
    public GameObject Current_Orb;

    public void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Shoot_Wand();
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

            if (Orb_Rigid_Body != null)
            {
                Orb_Rigid_Body.velocity = Raycast.direction * Mana_Orb_Speed;
            }
        }
    }

    public void OnTriggerEnter(Collider Collider)
    {
        Debug.Log("Collider Detected");
        Destroy_Orb();

        if (Collider.gameObject.CompareTag("Monster"))
        {
            Debug.Log("Monster Hit");
        }
    }

    public void Destroy_Orb()
    {
        Destroy(Current_Orb);
        Current_Orb = null;
    }
}
