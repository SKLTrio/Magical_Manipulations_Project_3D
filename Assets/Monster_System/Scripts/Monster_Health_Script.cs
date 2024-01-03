using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField]
    public GameObject Monster_Purple_Model;

    [SerializeField]
    public GameObject Monster_Damage_Indicator;

    [SerializeField]
    public TextMeshPro Monster_Damage_Indicator_Text;

    [SerializeField]
    public float Poison_Damage = 25f;

    [HideInInspector]
    public float Damage_Done;

    public void Start()
    {
        GameObject Monster_Wave_Object = GameObject.FindWithTag("Wave_Spawner");
        Monster_Wave = Monster_Wave_Object.GetComponent<Monster_Wave_Script>();

        Monster_Original_Model.SetActive(true);
        Monster_Red_Model.SetActive(false);
        Monster_Damage_Indicator.SetActive(false);

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
        Damage_Done = Player_Damage;
        Monster_Health -= Player_Damage;

        StartCoroutine("Monster_Damage_Taken");
    }

    IEnumerator Monster_Damage_Taken()
    {
        StartCoroutine("Damage_Indicator");

        Monster_Original_Model.SetActive(false);
        Monster_Red_Model.SetActive(true);

        yield return new WaitForSeconds(.1f);

        Monster_Red_Model.SetActive(false);
        Monster_Original_Model.SetActive(true);
    }

    IEnumerator Damage_Indicator()
    {
        Monster_Damage_Indicator_Text.text = "-" + Damage_Done.ToString();
        Monster_Damage_Indicator.SetActive(true);

        yield return new WaitForSeconds(1f);

        Monster_Damage_Indicator.SetActive(false);
    }

    public IEnumerator Monster_Poisoned()
    {
        Debug.Log("Monster has been poisoned!");

        float Monster_Poison_Duration = Random.Range(5, 10);
        float Time_Taken = 0f;

        while (Time_Taken < Monster_Poison_Duration)
        {
            Debug.Log(Poison_Damage + " has been taken");

            Damage_Done = Poison_Damage;
            Monster_Health -= Poison_Damage;

            StartCoroutine(Monster_Poison_Damage_Taken());

            yield return new WaitForSeconds(1f);

            Time_Taken += 1f;
        }
        Monster_Damage_Indicator.SetActive(false);

        Monster_Purple_Model.SetActive(false);
        Monster_Original_Model.SetActive(true);
    }

    IEnumerator Monster_Poison_Damage_Taken()
    {
        Debug.Log("Monster_Poison_Damage_Taken start");
        StartCoroutine("Damage_Indicator");

        Monster_Original_Model.SetActive(false);
        Monster_Purple_Model.SetActive(true);

        yield return new WaitForSeconds(.5f);

        Monster_Purple_Model.SetActive(false);
        Monster_Original_Model.SetActive(true);
    }
}