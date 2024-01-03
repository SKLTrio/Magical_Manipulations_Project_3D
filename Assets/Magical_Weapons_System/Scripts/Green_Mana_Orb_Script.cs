using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_Mana_Orb_Script : MonoBehaviour
{
    [HideInInspector]
    public Green_Wand_Script Green_Wand_Scr;

    [HideInInspector]
    public Monster_Movement_Script Movement_Script;

    [SerializeField]
    public string Wand_Prefab; 

    public void Start()
    {
        GameObject Wand_Obj = GameObject.Find(Wand_Prefab);
        Green_Wand_Scr = Wand_Obj.GetComponent<Green_Wand_Script>();
    }

    public void OnTriggerEnter(Collider Collider)
    {
        Green_Wand_Scr.Destroy_Orb();

        if (Collider.gameObject.CompareTag("Monster"))
        {
            Movement_Script = Collider.gameObject.GetComponent<Monster_Movement_Script>();
            
            StartCoroutine("Monster_Slow_Down");
        }
    }

    IEnumerator Monster_Slow_Down()
    {
        Movement_Script.Walk_Speed = 1f;

        yield return new WaitForSeconds(Random.Range(4, 9));

        Movement_Script.Walk_Speed = Movement_Script.Original_Walk_Speed;
    }
}
