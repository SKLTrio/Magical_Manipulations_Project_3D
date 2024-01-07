using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

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

    [SerializeField]
    public Image Golden_Mana_Added_UI_Image;

    [SerializeField]
    public TextMeshProUGUI Mana_Text;

    [HideInInspector]
    public bool Can_Turn_Blue = false;

    [HideInInspector]
    public bool Can_Turn_Gold = false;

    public void Start()
    {
        Current_Mana_Amount = Max_Mana_Amount;
        StartCoroutine("Regenerate_Mana");

    }

    public void Update()
    {
        Mana_Text.text = Current_Mana_Amount.ToString("N0") + "/100";

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
        Debug.Log("Taking mana: " + Mana_Amount);

        Current_Mana_Amount -= Mana_Amount;

        Current_Mana_Amount = Mathf.Max(Current_Mana_Amount, Min_Mana_Amount);

        Set_Mana_Slider();
    }

    public void Add_Mana(float Mana_Amount)
    {
        Current_Mana_Amount += Mana_Amount;

        Current_Mana_Amount = Mathf.Min(Current_Mana_Amount, Max_Mana_Amount);

        Set_Mana_Slider();

        Can_Turn_Blue = true;
        StartCoroutine("Set_Mana_Panel");
    }

    public void Add_Golden_Mana(float Mana_Amount)
    {
        Current_Mana_Amount += Mana_Amount;

        Current_Mana_Amount = Mathf.Min(Current_Mana_Amount, Max_Mana_Amount);

        Set_Mana_Slider();

        Can_Turn_Gold = true;
        StartCoroutine("Set_Gold_Mana_Panel");
    }

    IEnumerator Set_Mana_Panel()
    {
        if (Mana_Added_UI_Image != null)
        {
            float Image_Alpha;

            if (Can_Turn_Blue)
            {
                Can_Turn_Blue = false;

                for (Image_Alpha = 0f; Image_Alpha <= 1f; Image_Alpha += 0.1f)
                {
                    Can_Turn_Blue = false;
                    Color Current_Image_Colour = Mana_Added_UI_Image.color;
                    Current_Image_Colour.a = Image_Alpha;
                    Mana_Added_UI_Image.color = Current_Image_Colour;

                    yield return new WaitForSeconds(0.02f);
                }

                yield return new WaitForSeconds(.75f);

                for (Image_Alpha = 1f; Image_Alpha >= 0f; Image_Alpha -= 0.15f)
                {
                    Color Current_Image_Colour = Mana_Added_UI_Image.color;
                    Current_Image_Colour.a = Image_Alpha;
                    Mana_Added_UI_Image.color = Current_Image_Colour;

                    yield return new WaitForSeconds(0.02f);
                }

                Color Final_Image_Colour = Mana_Added_UI_Image.color;
                Final_Image_Colour.a = 0f;
                Mana_Added_UI_Image.color = Final_Image_Colour;

                Can_Turn_Blue = true;
            }
        }

        else
        {
            Debug.Log("Image Component not found!");
        }

    }

    IEnumerator Set_Gold_Mana_Panel()
    {
        if (Golden_Mana_Added_UI_Image != null)
        {
            float Image_Alpha;

            if (Can_Turn_Gold)
            {
                Can_Turn_Gold = false;

                for (Image_Alpha = 0f; Image_Alpha <= 1f; Image_Alpha += 0.1f)
                {
                    Can_Turn_Gold = false;
                    Color Current_Image_Colour = Golden_Mana_Added_UI_Image.color;
                    Current_Image_Colour.a = Image_Alpha;
                    Golden_Mana_Added_UI_Image.color = Current_Image_Colour;

                    yield return new WaitForSeconds(0.02f);
                }

                yield return new WaitForSeconds(.75f);

                for (Image_Alpha = 1f; Image_Alpha >= 0f; Image_Alpha -= 0.15f)
                {
                    Color Current_Image_Colour = Golden_Mana_Added_UI_Image.color;
                    Current_Image_Colour.a = Image_Alpha;
                    Golden_Mana_Added_UI_Image.color = Current_Image_Colour;

                    yield return new WaitForSeconds(0.02f);
                }

                Color Final_Image_Colour = Golden_Mana_Added_UI_Image.color;
                Final_Image_Colour.a = 0f;
                Golden_Mana_Added_UI_Image.color = Final_Image_Colour;

                Can_Turn_Gold = true;
            }
        }

        else
        {
            Debug.Log("Image Component not found!");
        }

    }
}
