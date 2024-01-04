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
    public TextMeshProUGUI Monster_Count_Text;

    [SerializeField]
    public TextMeshProUGUI Monster_Prepare_Text;

    [SerializeField]
    public TextMeshProUGUI Monster_Wave_Text;

    [SerializeField]
    public TextMeshProUGUI UI_Wave_Text;

    [HideInInspector]
    public bool Is_Spawning = false;

    [HideInInspector]
    public bool Is_First_Wave = false;

    [HideInInspector]
    public bool Is_Second_Wave = false;

    [HideInInspector]
    public bool Is_Third_Wave = false;

    [HideInInspector]
    public bool Next_Wave = false;

    public void Start()
    {
        Max_Monster_Count = 0;
        Monster_Count = 0;
        Wave_Count = 0;
        StartCoroutine("Wave_First_Timer"); 
    }

    public void Update()
    {
        UI_Wave_Text.text = "Wave: " + (Wave_Count + 1).ToString();

        Monster_Count_Text.text = "x " + Monster_Count.ToString();

        if (Wave_Count == 1 && !Is_Spawning && Is_Second_Wave)
        {
            StartCoroutine("Wave_Second_Timer");
        }

        if (Wave_Count == 2 && !Is_Spawning && Is_Third_Wave)
        {
            StartCoroutine("Wave_Third_Timer");
        }
    }

    public void First_Wave()
    {
        Debug.Log("First_Wave Started");

        Max_Monster_Count = 0;

        Wave_Count++;

        int Random_Monster_Count = Random.Range(1, 2); //Original: 5, 11

        Max_Monster_Count += Random_Monster_Count;

        Monster_Count = 0;

        InvokeRepeating("Spawn_Monster", 0f, Random.Range(1, 4));
    }

    public void Second_Wave()
    {
        Debug.Log("Second_Wave Started");

        Max_Monster_Count = 0;

        Wave_Count++;

        int Random_Monster_Count = Random.Range(2, 3); //Original: 15, 30

        Max_Monster_Count += Random_Monster_Count;

        Monster_Count = 0;

        InvokeRepeating("Spawn_Monster", 0f, Random.Range(1, 4));
    }

    public void Third_Wave()
    {
        Debug.Log("Third_Wave Started");

        Max_Monster_Count = 0;

        Wave_Count++;

        int Random_Monster_Count = Random.Range(3, 4); //Original: 35, 40

        Max_Monster_Count += Random_Monster_Count;

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

                if (Is_First_Wave)
                {
                    Is_First_Wave = false;
                    Is_Second_Wave = true;
                }
                else if (Is_Second_Wave)
                {
                    Is_Second_Wave = false;
                    Is_Third_Wave = true;
                }
                else if (Is_Third_Wave)
                {
                    Is_Third_Wave = false;
                }
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

    public IEnumerator Wave_First_Timer()
    {
        Is_First_Wave = true;

        Debug.Log("Wave_First_Timer Called");

        Monster_Prepare_Text.text = "Prepare to defend the Mana Crystal!";
        Monster_Prepare_Text.gameObject.SetActive(true);
        Debug.Log("Prepare Text Done");

        yield return new WaitForSeconds(2f); // original: 10

        Monster_Prepare_Text.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        Monster_Wave_Text.text = "First Wave Incoming!";
        Monster_Wave_Text.gameObject.SetActive(true);
        Debug.Log("Wave Text Done");

        yield return new WaitForSeconds(2f);

        Monster_Wave_Text.gameObject.SetActive(false);

        StartCoroutine("First_Wave");
    }

    public IEnumerator Wave_Second_Timer()
    {
        Debug.Log("Wave_Second_Timer Called");

        yield return new WaitForSeconds(0.5f);

        Monster_Wave_Text.text = "First Wave Ended!";
        Monster_Wave_Text.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f); // original: 15

        Monster_Wave_Text.text = "Second Wave Incoming!";
        Monster_Wave_Text.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        Monster_Wave_Text.gameObject.SetActive(false);

        StartCoroutine("Second_Wave");
    }

    public IEnumerator Wave_Third_Timer()
    {
        Debug.Log("Wave_Third_Timer Called");

        yield return new WaitForSeconds(0.5f);

        Monster_Wave_Text.text = "Second Wave Ended!";
        Monster_Wave_Text.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f); // original: 15

        Monster_Wave_Text.text = "Final Wave Incoming!";
        Monster_Wave_Text.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        Monster_Wave_Text.gameObject.SetActive(false);

        StartCoroutine("Third_Wave");
    }

}
