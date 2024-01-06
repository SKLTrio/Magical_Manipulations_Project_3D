using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Monster_Wave_Script : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> Monster_Type_Prefabs;

    [SerializeField]
    public Transform Parent_Object;

    [SerializeField]
    public List<Vector3> Monster_Spawning_Points;

    [HideInInspector]
    public float Spawn_X_Position;

    [HideInInspector]
    public float Spawn_Z_Position;

    [HideInInspector]
    public GameObject Monster_Type;

    [HideInInspector]
    public int Max_Monster_Count;

    [SerializeField]
    public int Monster_Count;

    [SerializeField]
    public int Wave_Count;

    [SerializeField]
    public float Wave_Timer;

    [SerializeField]
    public float Time_Between_Waves;

    [SerializeField]
    public TextMeshProUGUI Monster_Count_Text;



    public void Start()
    {
        Wave_Timer = 90;
        Max_Monster_Count = 0;
        Monster_Count = 0;
        Wave_Count = 1;
        Debug.Log("Start Wave #1");
        Start_Wave(10, 15);
    }

    public void Update()
    {
        Monster_Count_Text.text = Monster_Count.ToString();

        if (Monster_Count == 0 && Time.time > Wave_Timer)
        {
            Wave_Timer = 10f;
            StartCoroutine(Start_Wave_Timer(Wave_Timer));
            Debug.Log("Wave Timer Shortened");
        }

        if (Wave_Count == 2)
        {
            Wave_Timer = 180f;
            Start_Wave(20, 30);
            Debug.Log("Start Wave #2");

        }

        else if (Wave_Count == 3)
        {
            Wave_Timer = 240f;
            Start_Wave(35, 45);
            Debug.Log("Start Wave #3");
        }

        else if (Wave_Count == 4)
        {
            Debug.Log("Waves Finished");
        }
    }

    public void Start_Wave(int Lower_Range_Num, int Higher_Range_Num)
    {
        Debug.Log("First_Wave Started");

        Max_Monster_Count = 0;

        int Random_Monster_Count = Random.Range(Lower_Range_Num, Higher_Range_Num); //Original: 5, 11

        Max_Monster_Count += Random_Monster_Count;

        InvokeRepeating("Spawn_Monster", 0f, Random.Range(1, 4));
    }


    public void Spawn_Monster()
    {
        if (Monster_Count < Max_Monster_Count && Monster_Spawning_Points != null && Monster_Spawning_Points.Count > 0)
        {

            Random_Position_And_Type();

            Debug.Log("This new position is: " + Spawn_X_Position + ", 0, " + Spawn_Z_Position);

            if (Monster_Type == Monster_Type_Prefabs[0] || Monster_Type == Monster_Type_Prefabs[3] || Monster_Type == Monster_Type_Prefabs[6])
            {
                GameObject New_Monster = Instantiate(Monster_Type, new Vector3(Spawn_X_Position, 1.9f, Spawn_Z_Position), Quaternion.identity);
                New_Monster.transform.parent = Parent_Object;
            }

            else if (Monster_Type == Monster_Type_Prefabs[1] || Monster_Type == Monster_Type_Prefabs[4] || Monster_Type == Monster_Type_Prefabs[7])
            {
                GameObject New_Monster = Instantiate(Monster_Type, new Vector3(Spawn_X_Position, 4.51f, Spawn_Z_Position), Quaternion.identity);
                New_Monster.transform.parent = Parent_Object;
            }

            else if (Monster_Type == Monster_Type_Prefabs[2] || Monster_Type == Monster_Type_Prefabs[5] || Monster_Type == Monster_Type_Prefabs[8])
            {
                GameObject New_Monster = Instantiate(Monster_Type, new Vector3(Spawn_X_Position, 3f, Spawn_Z_Position), Quaternion.identity);
                New_Monster.transform.parent = Parent_Object;
            }

            Monster_Count += 1;

            if (Monster_Count >= Max_Monster_Count)
            {
                CancelInvoke("Spawn_Monster");
                Debug.Log("Spawning Finished");
                StartCoroutine(Start_Wave_Timer(Wave_Timer));
            }
        }
    }

    public void Random_Position_And_Type()
    {
        int Random_List_Position = Random.Range(0, Monster_Spawning_Points.Count);
        int Random_Monster_Type = Random.Range(0, Monster_Type_Prefabs.Count);

        Vector3 Random_Spawning_Point = Monster_Spawning_Points[Random_List_Position];
        Monster_Type = Monster_Type_Prefabs[Random_Monster_Type];

        Spawn_X_Position = Random_Spawning_Point.x;
        Spawn_Z_Position = Random_Spawning_Point.z;
    }

    public IEnumerator Start_Wave_Timer(float Time_Between_Waves)
    {
        yield return new WaitForSeconds(Time_Between_Waves);
        Wave_Count++;
    }
}
