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

    [SerializeField]
    public int Monster_Count;

    [SerializeField]
    public TextMeshProUGUI Monster_Count_Text;

    [SerializeField]
    public TextMeshProUGUI Monster_UI_Text;

    public void Start()
    {
        Monster_Count = 0;
        Monster_UI_Text.gameObject.SetActive(false);
        
        Debug.Log("Starting Wave");

        StartCoroutine("Start_Spawning");

        InvokeRepeating("Spawn_Monster", 20f, Random.Range(3, 8));
    }

    public void Update()
    {
        Monster_Count_Text.text = Monster_Count.ToString();
    }

    public IEnumerator Start_Spawning()
    {
        Debug.Log("Spawning Started");
        yield return new WaitForSeconds(2f);
        Monster_UI_Text.text = "Prepare to defend the Mana Crystal!";
        Monster_UI_Text.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        Monster_UI_Text.gameObject.SetActive(false);
        yield return new WaitForSeconds(5f);
        Monster_UI_Text.color = Color.red;
        Monster_UI_Text.text = "Monsters Incoming!";
        Monster_UI_Text.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        Monster_UI_Text.gameObject.SetActive(false);
        Spawn_Pre_Game_Monsters();
    }


    public void Spawn_Monster()
    {
        if (Monster_Spawning_Points != null && Monster_Spawning_Points.Count > 0)
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

    public void Spawn_Pre_Game_Monsters()
    {
        GameObject New_Monster = Instantiate(Monster_Type_Prefabs[0], new Vector3(-71.2f, 1.9f, -6.5f), Quaternion.identity);
        New_Monster.transform.parent = Parent_Object;

        Monster_Count++;

        GameObject New_Monster_2 = Instantiate(Monster_Type_Prefabs[7], new Vector3(44.17f, 4.51f, 17.68f), Quaternion.identity);
        New_Monster.transform.parent = Parent_Object;

        Monster_Count++;
    }
}
