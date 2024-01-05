using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana_Spawn_Script : MonoBehaviour
{
    [SerializeField]
    public GameObject Mana_Consumable_Prefab;

    [SerializeField]
    public GameObject Golden_Mana_Consumable_Prefab;

    [SerializeField]
    public Mana_Crystal_Health_Script Crystal_Health_Script;

    [SerializeField]
    public List<Vector3> Spawn_Positions;

    [HideInInspector]
    public float Spawn_Time;

    [HideInInspector]
    public bool Can_Spawn = false;

    public void Start()
    {
        Can_Spawn = true;
    }

    public void Update()
    {
        if (Can_Spawn)
        {
            if (Crystal_Health_Script.Current_Health <= 100 && Crystal_Health_Script.Current_Health >= 85)
            {
                Spawn_Time = Random.Range(5, 11);
                StartCoroutine("Spawn_Mana_Consumable");
            }

            else if (Crystal_Health_Script.Current_Health < 85 && Crystal_Health_Script.Current_Health >= 50)
            {
                Spawn_Time = Random.Range(10, 21);
                StartCoroutine("Spawn_Mana_Consumable");
            }

            else if (Crystal_Health_Script.Current_Health < 50 && Crystal_Health_Script.Current_Health >= 25)
            {
                Spawn_Time = Random.Range(20, 41);
                StartCoroutine("Spawn_Mana_Consumable");
            }

            else if (Crystal_Health_Script.Current_Health < 25)
            {
                Can_Spawn = false;
            }
        }
    }

    public IEnumerator Spawn_Mana_Consumable()
    {
        Can_Spawn = false;

        yield return new WaitForSeconds(Spawn_Time);

        if (Spawn_Positions != null && Spawn_Positions.Count > 0)
        {
            int Chance_Value = Random.Range(1, 105);

            if (Chance_Value <= 5)
            {
                Vector3 Random_Point = Spawn_Positions[Random.Range(0, Spawn_Positions.Count)];
                Instantiate(Golden_Mana_Consumable_Prefab, Random_Point, Quaternion.identity);
            }

            else
            {
                Vector3 Random_Point = Spawn_Positions[Random.Range(0, Spawn_Positions.Count)];
                Instantiate(Mana_Consumable_Prefab, Random_Point, Quaternion.identity);
            }  
        }

        Can_Spawn = true;
    }
}
