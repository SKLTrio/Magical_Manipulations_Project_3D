using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster_Health_Script : MonoBehaviour
{
    [SerializeField]
    public float Monster_Health;

    [SerializeField]
    Monster_Wave_Script Monster_Wave;

    public void Start()
    {
        GameObject Monster_Wave_Object = GameObject.FindWithTag("Wave_Spawner");
        Monster_Wave = Monster_Wave_Object.GetComponent<Monster_Wave_Script>();
    }

    public void Update()
    {
        if (Monster_Health <= 0)
        {
            Destroy(gameObject);
            Monster_Wave.Monster_Count--;
        }
    }
}
