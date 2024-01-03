using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple_Mana_Orb_Script : MonoBehaviour
{
    [SerializeField]
    public string Wand_Prefab;

    [HideInInspector]
    public Purple_Wand_Script Purple_Wand_Scr;

    [HideInInspector]
    public Monster_Movement_Script Movement_Script;

    public void Start()
    {
        GameObject Wand_Obj = GameObject.Find(Wand_Prefab);
        Purple_Wand_Scr = Wand_Obj.GetComponent<Purple_Wand_Script>();
    }

    public void OnTriggerEnter(Collider Collider)
    {
        Purple_Wand_Scr.Destroy_Orb();

        if (Collider.gameObject.CompareTag("Monster"))
        {
            Debug.Log("Monster Found");
            Monster_Health_Script Health_Script = Collider.gameObject.GetComponent<Monster_Health_Script>();
            Movement_Script = Collider.gameObject.GetComponent<Monster_Movement_Script>();

            Health_Script.StartCoroutine("Monster_Poisoned");
            StartCoroutine("Monster_Poison_Slow_Down");
        }
    }

    IEnumerator Monster_Poison_Slow_Down()
    {
        Movement_Script.Walk_Speed = 3f;

        yield return new WaitForSeconds(Random.Range(6, 9));

        Movement_Script.Walk_Speed = Movement_Script.Original_Walk_Speed;
    }
}
