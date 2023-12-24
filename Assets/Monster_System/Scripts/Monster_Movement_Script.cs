using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Monster_Movement_Script : MonoBehaviour
{
    [SerializeField]
    public float Walk_Speed;

    [SerializeField]
    public float Chase_Speed;

    [SerializeField]
    public float Turn_Speed;

    [SerializeField]
    public float Walk_Point_Radius;

    [SerializeField]
    public float Chase_Distance;

    [SerializeField]
    public float Crystal_Chase_Distance;

    [SerializeField]
    public float Obstacle_Avoidance_Distance;

    [HideInInspector]
    private Transform Player_Object;

    [HideInInspector]
    private Transform Mana_Crytal_Object;

    [HideInInspector]
    private Vector3 Walk_Destination;

    [HideInInspector]
    private bool Is_Walking;

    [HideInInspector]
    private bool Is_Chasing_Player;

    [HideInInspector]
    private bool Is_Chasing_Crystal;

    [SerializeField]
    public Animator Monster_Animator;

    public void Start()
    {
        Player_Object = GameObject.FindGameObjectWithTag("Player").transform;
        Mana_Crytal_Object = GameObject.FindGameObjectWithTag("Mana_Crystal").transform;

        Set_Random_Destination();
    }

    public void Update()
    {
        float Distance_To_Player = Vector3.Distance(transform.position, Player_Object.position);
        float Distance_To_Crystal = Vector3.Distance(transform.position, Mana_Crytal_Object.position);

        if (Distance_To_Player <= Chase_Distance)
        {
            Is_Chasing_Player = true;
            Is_Chasing_Crystal = false;
            Is_Walking = true;
            Monster_Animator.SetBool("Is_Walking", true);
        }

        else if (Distance_To_Crystal <= Crystal_Chase_Distance)
        {
            Is_Chasing_Player = false;
            Is_Chasing_Crystal = true;
            Is_Walking = true;
            Monster_Animator.SetBool("Is_Walking", true);
        }

        else if (Distance_To_Player > Chase_Distance * 1.5f)
        {
            Is_Chasing_Player = false;
            Is_Chasing_Crystal = false;
            Set_Random_Destination();
            Is_Walking = true;
            Monster_Animator.SetBool("Is_Walking", true);
        }

        else if (Distance_To_Crystal > Crystal_Chase_Distance * 1.5f)
        {
            Is_Chasing_Player = false;
            Is_Chasing_Crystal = false;
            Set_Random_Destination();
            Is_Walking = true;
            Monster_Animator.SetBool("Is_Walking", true);
        }

        if (Is_Chasing_Player)
        {
            Chase_Player();
        }

        else if (Is_Chasing_Crystal)
        {
            Run_To_Crystal();
        }

        else
        {
            Patrol_Around();
        }
    }

    void Patrol_Around()
    {
        Quaternion Monster_Rotation = Quaternion.LookRotation(Walk_Destination - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Monster_Rotation, Turn_Speed * Time.deltaTime);

        transform.Translate(Vector3.forward * Walk_Speed * Time.deltaTime);

        float Distance_To_Destination = Vector3.Distance(transform.position, Walk_Destination);

        if (Distance_To_Destination < 1.0f || Check_For_Obstacle())
        {
            Set_Random_Destination();
        }
    }

    void Chase_Player()
    {
        Vector3 Direction_To_Player = Player_Object.position - transform.position;
        Direction_To_Player.y = 0;
        Quaternion Rotation = Quaternion.LookRotation(Direction_To_Player);
        transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, Turn_Speed * Time.deltaTime);

        transform.Translate(Vector3.forward * Chase_Speed * Time.deltaTime);
    }

    void Run_To_Crystal()
    {
        Vector3 Direction_To_Crystal = Mana_Crytal_Object.position - transform.position;
        Direction_To_Crystal.y = 0;
        Quaternion Rotation = Quaternion.LookRotation(Direction_To_Crystal);
        transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, Turn_Speed * Time.deltaTime);

        transform.Translate(Vector3.forward * Chase_Speed * Time.deltaTime);
    }

    void Set_Random_Destination()
    {
        float Random_X_Position = Random.Range(-Walk_Point_Radius, Walk_Point_Radius);
        float Random_Z_Position = Random.Range(-Walk_Point_Radius, Walk_Point_Radius);

        Walk_Destination = new Vector3(transform.position.x + Random_X_Position, transform.position.y, transform.position.z + Random_Z_Position);
    }

    public bool Check_For_Obstacle()
    {
        RaycastHit Hit;

        if (Physics.Raycast(transform.position, transform.forward, out Hit, Obstacle_Avoidance_Distance))
        {
            if (Hit.collider.CompareTag("Monster_Obstacle"))
            {
                return true;
            }
        }

        return false;
    }
}
