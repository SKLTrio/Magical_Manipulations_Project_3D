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
    public Player_Health_Script Player_Health;

    [SerializeField]
    public Texture2D Cursor_Texture;

    [SerializeField]
    public float Timer_Starting_Seconds;

    [SerializeField]
    public TextMeshProUGUI Game_Timer_Text;

    [HideInInspector]
    public float Current_Time;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public void Start()
    {
        Cursor.SetCursor(Cursor_Texture, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void Update()
    {
        Current_Time = Timer_Starting_Seconds -= Time.deltaTime;
        Update_Timer(Current_Time);

        if (Current_Time <= 0 && Player_Health.Current_Health > 0)
        {
            Menu_Controller_Script.Open_Win_Panel();
        }
    }

    public void Update_Timer(float Current_Time)
    {
        int Seconds = (int)Current_Time;
        Game_Timer_Text.text = System.TimeSpan.FromSeconds(Seconds).ToString("mm':'ss");
    }
}
