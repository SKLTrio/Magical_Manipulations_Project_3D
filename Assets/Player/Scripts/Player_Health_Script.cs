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
    public int Player_Lives;

    [SerializeField]
    public Vector3 Player_Respawn_Point;

    [SerializeField]
    public Slider Health_Slider;

    [SerializeField]
    public Image UI_Damage_Image;

    [SerializeField]
    public Image UI_Respawn_Transition_Image;

    [SerializeField]
    public GameObject UI_Life_1;

    [SerializeField]
    public GameObject UI_Life_2;

    [SerializeField]
    public GameObject UI_Life_3;

    [SerializeField]
    public Player_Mana_Script Mana_Script;

    [HideInInspector]
    public float Red_Image_Alpha; 

    [HideInInspector]
    public bool Can_Turn_Red;

    [HideInInspector]
    public List<Monster_Movement_Script> Current_Monsters = new List<Monster_Movement_Script>();

    //[SerializeField]
    //public Menu_Controller_Script Menu_Controller;

    public void Start()
    {
        Player_Lives = 3;
        Current_Health = Max_Health;
        Can_Turn_Red = true;
    }

    public void Update()
    {
        if (Current_Health < 0)
        {
            Current_Health = 0;
        }

        if (Current_Health == 0)
        {
            Player_Lives--;
            Respawn_Player();
        }

        if (Player_Lives == 3)
        {
            UI_Life_1.SetActive(true);
            UI_Life_2.SetActive(true);
            UI_Life_3.SetActive(true);
        }

        else if (Player_Lives == 2)
        {
            UI_Life_1.SetActive(true);
            UI_Life_2.SetActive(true);
            UI_Life_3.SetActive(false);
        }

        else if (Player_Lives == 1)
        {
            UI_Life_1.SetActive(true);
            UI_Life_2.SetActive(false);
            UI_Life_3.SetActive(false);
        }

        else if (Player_Lives <= 0)
        {
            UI_Life_1.SetActive(false);
            UI_Life_2.SetActive(false);
            UI_Life_3.SetActive(false);

            On_Death();
        }
    }

    public void Take_Damage(float Monster_Damage)
    {
        Current_Health -= Monster_Damage;

        Current_Health = Mathf.Max(Current_Health, Min_Health);

        Set_Health_Slider();

        StartCoroutine(Set_Damage_Panel());
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
            if (Can_Turn_Red)
            {
                for (Red_Image_Alpha = 0f; Red_Image_Alpha <= 1f; Red_Image_Alpha += 0.1f)
                {
                    Can_Turn_Red = false;
                    Color Current_Image_Colour = UI_Damage_Image.color;
                    Current_Image_Colour.a = Red_Image_Alpha;
                    UI_Damage_Image.color = Current_Image_Colour;

                    yield return new WaitForSeconds(0.02f);
                }

                yield return new WaitForSeconds(.75f);

                for (Red_Image_Alpha = 1f; Red_Image_Alpha >= 0f; Red_Image_Alpha -= 0.1f)
                {
                    Color Current_Image_Colour = UI_Damage_Image.color;
                    Current_Image_Colour.a = Red_Image_Alpha;
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

    public void Respawn_Player()
    {
        Red_Image_Alpha = 0f;
        Can_Turn_Red = false;
        Current_Health = Max_Health;
        Mana_Script.Current_Mana_Amount = Mana_Script.Max_Mana_Amount;
        Set_Health_Slider();
        Mana_Script.Set_Mana_Slider();
        StartCoroutine("Respawn_Transition");
        transform.position = Player_Respawn_Point;
    }

    public IEnumerator Respawn_Transition()
    {
        Pause_Current_Monsters(true);

        float Image_Alpha;

        for (Image_Alpha = 0f; Image_Alpha <= 2f; Image_Alpha += 0.1f)
        {
            Color Current_Image_Colour = UI_Respawn_Transition_Image.color;
            Current_Image_Colour.a = Image_Alpha;
            UI_Respawn_Transition_Image.color = Current_Image_Colour;

            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(.5f);

        for (Image_Alpha = 2f; Image_Alpha >= 0f; Image_Alpha -= 0.1f)
        {
            Color Current_Image_Colour = UI_Respawn_Transition_Image.color;
            Current_Image_Colour.a = Image_Alpha;
            UI_Respawn_Transition_Image.color = Current_Image_Colour;

            yield return new WaitForSeconds(0.02f);
        }

        Color Final_Image_Colour = UI_Respawn_Transition_Image.color;
        Final_Image_Colour.a = 0f;
        UI_Respawn_Transition_Image.color = Final_Image_Colour;
        Pause_Current_Monsters(false);
        Can_Turn_Red = true;
    }

    public void Pause_Current_Monsters(bool Pause_Monsters)
    {
        GameObject[] Monsters = GameObject.FindGameObjectsWithTag("Monster");

        foreach (GameObject Monster_Object in Monsters)
        {
            Monster_Movement_Script Movement_Script = Monster_Object.GetComponent<Monster_Movement_Script>();

            if (Movement_Script != null)
            {
                if (Pause_Monsters)
                {
                    Movement_Script.Pause_Monster();
                }

                else
                {
                    Movement_Script.Resume_Monster();
                }
            }

        }
    }

    public void On_Death()
    {
        Debug.Log("GAME OVER - YOU DIED");
        Time.timeScale = 0f;
        //Menu_Controller_Script.Open_Death_Panel();
    }

}

