using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana_Consumable_Script : MonoBehaviour
{
    [SerializeField]
    public float Mana_Give_Amount;

    [SerializeField]
    public float Movement_Speed;

    [SerializeField]
    public GameObject Player_Object;

    public void Start()
    {
        Player_Object = GameObject.FindWithTag("Player");
    }

    public void Update()
    {
        Move_To_Player();
    }

    private void OnTriggerEnter(Collider Collider)
    {
        if (Collider.CompareTag("Player"))
        {
            Player_Mana_Script Mana_Script = Collider.GetComponent<Player_Mana_Script>();
            Mana_Script.Add_Mana(Mana_Give_Amount);
        }
    }

    public void Move_To_Player()
    {
        if (Player_Object != null)
        {
            Vector3 Player_Position = Player_Object.transform.position;
            //Vector3 
        }
    }
}
