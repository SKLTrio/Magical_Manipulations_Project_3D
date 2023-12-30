using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Player_Mana_Script : MonoBehaviour
{
    [SerializeField]
    public float Max_Mana_Amount;

    [SerializeField]
    public float Min_Mana_Amount;

    [SerializeField]
    public float Current_Mana_Amount;

    [SerializeField]
    public float Mana_Increase_Amount;

    [SerializeField]
    public Slider Mana_Slider;

    [SerializeField]
    public Image Mana_Added_UI_Image;

    [HideInInspector]
    public bool Can_Turn_Blue;

    public void Start()
    {
        Current_Mana_Amount = Max_Mana_Amount;
        StartCoroutine("Regenerate_Mana");
    }

    public void Update()
    {
        if (Current_Mana_Amount <= 0)
        {
            Current_Mana_Amount = 0;
        }
    }

    public IEnumerator Regenerate_Mana()
    {
        while (true)
        {
            if (Current_Mana_Amount < Max_Mana_Amount)
            {
                Current_Mana_Amount += Mana_Increase_Amount * Time.deltaTime;

                Set_Mana_Slider();
            }

            yield return null;
        }
    }

    public void Set_Mana_Slider()
    {
        if (Mana_Slider != null)
        {
            Mana_Slider.value = Normalised_Hit_Points();
        }
    }

    public float Normalised_Hit_Points()
    {
        return Current_Mana_Amount / Max_Mana_Amount;
    }

    public void Take_Mana(float Mana_Amount)
    {
        Current_Mana_Amount -= Mana_Amount;

        Current_Mana_Amount = Mathf.Max(Current_Mana_Amount, Min_Mana_Amount);

        Set_Mana_Slider();
    }

    public void Add_Mana(float Mana_Amount)
    {
        Current_Mana_Amount += Mana_Amount;

        Current_Mana_Amount = Mathf.Max(Current_Mana_Amount, Max_Mana_Amount);

        Set_Mana_Slider();

        StartCoroutine("Set_Mana_Panel");
    }

    IEnumerator Set_Mana_Panel()
    {
        if (Mana_Added_UI_Image != null)
        {
            float Image_Alpha;

            if (Can_Turn_Blue)
            {
                for (Image_Alpha = 0f; Image_Alpha <= 0.5f; Image_Alpha += 0.01f)
                {
                    Can_Turn_Blue = false;
                    Color Current_Image_Colour = Mana_Added_UI_Image.color;
                    Current_Image_Colour.a = Image_Alpha;
                    Mana_Added_UI_Image.color = Current_Image_Colour;

                    yield return new WaitForSeconds(0.05f);
                }

                yield return new WaitForSeconds(.75f);

                for (Image_Alpha = 0.1f; Image_Alpha >= 0f; Image_Alpha -= 0.01f)
                {
                    Color Current_Image_Colour = Mana_Added_UI_Image.color;
                    Current_Image_Colour.a = Image_Alpha;
                    Mana_Added_UI_Image.color = Current_Image_Colour;

                    yield return new WaitForSeconds(0.01f);
                    Can_Turn_Blue = true;
                }
            }
        }

        else
        {
            Debug.Log("Image Component not found!");
        }

    }
}
