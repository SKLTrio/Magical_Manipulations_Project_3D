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

    [SerializeField]
    public GameObject Monster_Original_Model;

    [SerializeField]
    public GameObject Monster_Red_Model;

    public void Start()
    {
        GameObject Monster_Wave_Object = GameObject.FindWithTag("Wave_Spawner");
        Monster_Wave = Monster_Wave_Object.GetComponent<Monster_Wave_Script>();

        Monster_Original_Model.SetActive(true);
        Monster_Red_Model.SetActive(false);

    }

    public void Update()
    {
        if (Monster_Health <= 0)
        {
            Destroy(gameObject);
            Monster_Wave.Monster_Count--;
        }
    }

    public void Take_Damage(float Player_Damage)
    {
        Monster_Health -= Player_Damage;

        StartCoroutine("Monster_Turn_Red");
    }

    IEnumerator Monster_Turn_Red()
    {
        Monster_Original_Model.SetActive(false);
        Monster_Red_Model.SetActive(true);

        yield return new WaitForSeconds(.1f);

        Monster_Red_Model.SetActive(false);
        Monster_Original_Model.SetActive(true);
    }
}
