using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    public int Max_Monster_Count;

    [SerializeField]
    public int Monster_Count;

    [SerializeField]
    public int Wave_Count;

    public void Start()
    {
        Invoke("Next_Wave", 5f);
    }

    public void Next_Wave()
    {
        Wave_Count--;

        Max_Monster_Count += 15;

        Monster_Count = 0;

        InvokeRepeating("Spawn_Monster", 0f, Random.Range(5, 16));
    }

    public void Spawn_Monster()
    {
        if (Monster_Count < Max_Monster_Count && Monster_Spawning_Points != null && Monster_Spawning_Points.Count > 0)
        {
            Random_Position_And_Type();

            Debug.Log("This new position is: " + Spawn_X_Position + ", 0, " + Spawn_Z_Position);

            if (Monster_Type == Monster_Type_Prefabs[0])
            {
                GameObject New_Monster = Instantiate(Monster_Type, new Vector3(Spawn_X_Position, 1.9f, Spawn_Z_Position), Quaternion.identity);
                New_Monster.transform.parent = Parent_Object;
            }

            else if (Monster_Type == Monster_Type_Prefabs[1])
            {
                GameObject New_Monster = Instantiate(Monster_Type, new Vector3(Spawn_X_Position, 4.51f, Spawn_Z_Position), Quaternion.identity);
                New_Monster.transform.parent = Parent_Object;
            }

            else if (Monster_Type == Monster_Type_Prefabs[2])
            {
                GameObject New_Monster = Instantiate(Monster_Type, new Vector3(Spawn_X_Position, 3f, Spawn_Z_Position), Quaternion.identity);
                New_Monster.transform.parent = Parent_Object;
            }

            Monster_Count += 1;

            if (Monster_Count >= Max_Monster_Count)
            {
                CancelInvoke("Spawn_Monster");
                Invoke("Next_Wave", 20);
                Debug.Log("Next Wave Incoming");
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
}
