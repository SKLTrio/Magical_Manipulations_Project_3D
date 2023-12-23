using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ground_Check : MonoBehaviour
{
    [SerializeField]
    public Player_Locomotion Player_Locomotion_Script;

    private void OnTriggerEnter(Collider Collider)
    {
        if (Collider.gameObject == Player_Locomotion_Script.gameObject)
        {
            return;
        }

        Player_Locomotion_Script.Grounded(true);
    }

    private void OnTriggerExit(Collider Collider)
    {
        if (Collider.gameObject == Player_Locomotion_Script.gameObject)
        {
            return;
        }

        Player_Locomotion_Script.Grounded(false);
    }

    private void OnTriggerStay(Collider Collider)
    {
        if (Collider.gameObject == Player_Locomotion_Script.gameObject)
        {
            return;
        }

        Player_Locomotion_Script.Grounded(true);
    }
}