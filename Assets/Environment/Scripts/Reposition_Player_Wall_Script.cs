using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition_Player_Wall_Script : MonoBehaviour
{
    [SerializeField]
    public Vector3 Player_Reposition_Coords = Vector3.zero;

    private void OnTriggerEnter(Collider Collider)
    {
        if (Collider.CompareTag("Player"))
        {
            Collider.gameObject.transform.position = Player_Reposition_Coords;
            Debug.Log("You can't go further than this point!");
            //Set it up so that text pops up on screen saying the above!
        }
    }
}
