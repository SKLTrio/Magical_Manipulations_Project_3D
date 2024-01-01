using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player_Health_Script : MonoBehaviour
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
    public Image UI_Damage_Image;

    [HideInInspector]
    public bool Can_Turn_Red;

    //[SerializeField]
    //public Menu_Controller_Script Menu_Controller;

    public void Start()
    {
        Current_Health = Max_Health;
        Can_Turn_Red = true;
    }

    public void Update()
    {
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

        StartCoroutine(Set_Damage_Panel());

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

    public IEnumerator Set_Damage_Panel()
    {
        if (UI_Damage_Image != null)
        {
            float Image_Alpha;

            if (Can_Turn_Red)
            {
                for (Image_Alpha = 0f; Image_Alpha <= 1f; Image_Alpha += 0.1f)
                {
                    Can_Turn_Red = false;
                    Color Current_Image_Colour = UI_Damage_Image.color;
                    Current_Image_Colour.a = Image_Alpha;
                    UI_Damage_Image.color = Current_Image_Colour;

                    yield return new WaitForSeconds(0.02f);
                }

                yield return new WaitForSeconds(.75f);

                for (Image_Alpha = 1f; Image_Alpha >= 0f; Image_Alpha -= 0.1f)
                {
                    Color Current_Image_Colour = UI_Damage_Image.color;
                    Current_Image_Colour.a = Image_Alpha;
                    UI_Damage_Image.color = Current_Image_Colour;

                    yield return new WaitForSeconds(0.02f);
                }

                Color Final_Image_Colour = UI_Damage_Image.color;
                Final_Image_Colour.a = 0f;
                UI_Damage_Image.color = Final_Image_Colour;

                Can_Turn_Red = true;
            }     
        }

        else
        {
            Debug.Log("Image Component not found!");
        }

    }

    public void On_Death()
    {
        Debug.Log("GAME OVER - YOU DIED");
        //Menu_Controller_Script.Open_Death_Panel();
    }

}

