using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI Game_Timer_Text;

    public void Start()
    {
        Update_Timer(360f);
    }

    public void Update_Timer(float Current_Time)
    {
        int Seconds = (int)Current_Time;
        Game_Timer_Text.text = System.TimeSpan.FromSeconds(Seconds).ToString("mm':'ss");
    }
}
