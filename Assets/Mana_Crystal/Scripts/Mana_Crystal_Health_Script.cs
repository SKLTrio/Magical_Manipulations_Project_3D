using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Mana_Crystal_Health_Script : MonoBehaviour
{
    [SerializeField]
    public float Max_Health = 100f;

    [SerializeField]
    public float Min_Health = 0f;

    [SerializeField]
    public float Current_Health;

    [SerializeField]
    public Slider Health_Slider;

    [SerializeField]
    public TextMeshProUGUI Health_Text;

    [SerializeField]
    public Menu_Controller Menu_Controller_Script;

    public void Start()
    {
        Current_Health = Max_Health;
    }

    public void Update()
    {
        Health_Text.text = Current_Health.ToString() + "/100";

        if (Current_Health < 0)
        {
            Current_Health = 0;
        }
    }

    public void Take_Damage(float Monster_Damage)
    {
        Current_Health -= Monster_Damage;

        Current_Health = Mathf.Max(Current_Health, Min_Health);

        Set_Health_Slider();

        if (Current_Health <= Min_Health)
        {
            On_Death();
        }

    }

    public void Set_Health_Slider()
    {
        if (Health_Slider != null)
        {
            Health_Slider.value = Normalised_Hit_Points();
        }
    }

    public float Normalised_Hit_Points()
    {
        return Current_Health / Max_Health;
    }

    public void On_Death()
    {
        Debug.Log("GAME OVER - The Crystal Died");
        Menu_Controller_Script.Open_Game_Over_Panel();
    }
}
