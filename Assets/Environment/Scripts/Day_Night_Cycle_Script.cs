using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_Night_Cycle : MonoBehaviour
{
    [SerializeField]
    public float Light_Rotation_Speed = 1.0f;

    [SerializeField]
    public float Day_Fog_Amount = 1.0f;

    [SerializeField]
    public float Night_Fog_Amount = 1.0f;

    public void Start()
    {
        float Time_In_Seconds = 360f / Light_Rotation_Speed;
        Debug.Log("Day and Night Cycle Duration: " + Time_In_Seconds + " Seconds");
    }

    public void Update()
    {
        transform.Rotate(Vector3.right * Light_Rotation_Speed * Time.deltaTime);
        Update_Fog();
    }

    public void Update_Fog()
    {
        float Current_Rotation = transform.eulerAngles.x;

        if (Current_Rotation < 180f)
        {
            RenderSettings.fogDensity = Day_Fog_Amount;
        }

        else
        {
            RenderSettings.fogDensity = Night_Fog_Amount;
        }
    }
}
