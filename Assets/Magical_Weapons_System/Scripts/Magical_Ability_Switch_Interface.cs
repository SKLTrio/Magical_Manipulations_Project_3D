using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Magical_Ability_Switch_Interface : MonoBehaviour
{
    [SerializeField]
    public int Selected_Ability = 0;

    [SerializeField]
    public GameObject[] Ability_Image;

    [SerializeField]
    public GameObject[] Ability_Ammo_Image;

    [SerializeField]
    public Menu_Controller Menu_Controller_Script;

    public void Start()
    {
        Select_Ability();
    }

    public void Update()
    {
        if (!Menu_Controller_Script.Is_Game_Paused)
        {
            if (!Menu_Controller_Script.Is_Game_Done)
            {
                int Previously_Selected_Ability = Selected_Ability;

                float Scroll_Wheel_Value = Input.mouseScrollDelta.y;

                if (Scroll_Wheel_Value < 0f)
                {
                    if (Selected_Ability >= transform.childCount - 1)
                    {
                        Selected_Ability = 0;
                    }

                    else
                    {
                        Selected_Ability++;
                    }
                }

                if (Scroll_Wheel_Value > 0f)
                {
                    if (Selected_Ability <= 0)
                    {
                        Selected_Ability = transform.childCount - 1;
                    }

                    else
                    {
                        Selected_Ability--;
                    }
                }

                if (Previously_Selected_Ability != Selected_Ability)
                {
                    Select_Ability();
                }
            }
        }
    }

    public void Select_Ability()
    {
        int i = 0;

        foreach (Transform Ability in transform)
        {
            if (i == Selected_Ability)
            {
                Ability.gameObject.SetActive(true);
                Ability_Image[i].SetActive(true);
                Ability_Ammo_Image[i].SetActive(true);
            }

            else
            {
                Ability.gameObject.SetActive(false);
                Ability_Image[i].SetActive(false);
                Ability_Ammo_Image[i].SetActive(false);
            }

            i++;
        }
    }
}
