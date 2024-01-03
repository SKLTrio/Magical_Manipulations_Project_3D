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
    public int Monster_Wave_Increase_Count;

    [SerializeField]
    public int Monster_Count;

    [SerializeField]
    public int Wave_Count;

    public void Start()
    {
        StartCoroutine("Wave_Start_Timer");     
    }

    public void First_Wave()
    {
        Wave_Count--;

        Max_Monster_Count += Monster_Wave_Increase_Count;

        Monster_Count = 0;

        InvokeRepeating("Spawn_Monster", 0f, Random.Range(1, 4));
    }

    public void Second_Wave()
    {
        Wave_Count--;

        Max_Monster_Count += Monster_Wave_Increase_Count;

        Monster_Count = 0;

        InvokeRepeating("Spawn_Monster", 0f, Random.Range(1, 4));
    }

    public void Third_Wave()
    {
        Wave_Count--;

        Max_Monster_Count += Monster_Wave_Increase_Count;

        Monster_Count = 0;

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

    public IEnumerator Wave_Start_Timer()
    {
        yield return new WaitForSeconds(15f);

        Invoke("First_Wave", 0f);
    }
}
