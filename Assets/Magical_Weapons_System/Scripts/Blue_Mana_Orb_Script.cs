using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_Mana_Orb_Script : MonoBehaviour
{
    [HideInInspector]
    public Blue_Wand_Script Blue_Wand_Scr;

    [SerializeField]
    public string Wand_Prefab; 

    public void Start()
    {
        GameObject Wand_Obj = GameObject.Find(Wand_Prefab);
        Blue_Wand_Scr = Wand_Obj.GetComponent<Blue_Wand_Script>();
    }

    public void OnTriggerEnter(Collider Collider)
    {
        Debug.Log("Collider Detected");
        Blue_Wand_Scr.Destroy_Orb();

        if (Collider.gameObject.CompareTag("Monster"))
        {
            Monster_Health_Script Health_Script = Collider.gameObject.GetComponent<Monster_Health_Script>();

            Health_Script.Take_Damage(Blue_Wand_Scr.Wand_Damage);
        }
    }
}
