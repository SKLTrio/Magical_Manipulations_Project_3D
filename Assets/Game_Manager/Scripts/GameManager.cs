using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager Instance { get; private set; }

    [SerializeField]
    public Menu_Controller Menu_Controller_Script;

    [SerializeField]
    public UI_Manager UI_Manager_Script;

    [SerializeField]
    public Player_Health_Script Player_Health;

    [SerializeField]
    public float Timer_Starting_Seconds = 360f;

    [HideInInspector]
    public float Current_Time;

    [HideInInspector]
    private float Starting_Timer_Amount;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public void Update()
    {
        if (!Menu_Controller_Script.Is_Game_Paused && !Menu_Controller_Script.Is_Game_Done)
        {
            Current_Time = Timer_Starting_Seconds -= Time.deltaTime;
            UI_Manager_Script.Update_Timer(Current_Time);

            if (Current_Time <= 0 && Player_Health.Current_Health > 0)
            {
                Menu_Controller_Script.Open_Win_Panel();
            }
        }
    }
}
