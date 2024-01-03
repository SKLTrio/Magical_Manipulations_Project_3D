using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Movement_Script : MonoBehaviour
{
    [SerializeField]
    public float Walk_Speed;

    [SerializeField]
    public float Turn_Speed;

    [SerializeField]
    public float Stopping_Radius;

    [SerializeField]
    public float Obstacle_Avoidance_Distance;

    [HideInInspector]
    public Transform Player_Object;

    [HideInInspector]
    public Transform Mana_Crytal_Object;

    [HideInInspector]
    public Transform Monster_Target;

    [HideInInspector]
    private bool Is_Walking;

    [HideInInspector]
    public float Original_Walk_Speed;

    [SerializeField]
    public Animator Monster_Animator;

    [SerializeField]
    public GameObject Monster_Growl_Parent_Object;

    [SerializeField]
    public AudioClip Monster_Growl_1_Clip;

    [SerializeField]
    public AudioClip Monster_Growl_2_Clip;

    private float Monster_Y_Position;

    public void Start()
    {
        Player_Object = GameObject.FindGameObjectWithTag("Player").transform;
        Mana_Crytal_Object = GameObject.FindGameObjectWithTag("Mana_Crystal").transform;

        Original_Walk_Speed = Walk_Speed;

        if (Random.Range(0f, 1f) > 0.35f)
        {
            Monster_Target = Player_Object;
        }
        else
        {
            Monster_Target = Mana_Crytal_Object;
        }

        Set_Monster_Destination(Monster_Target.position);

        Monster_Y_Position = transform.position.y;
    }

    public void Update()
    {
        float Distance_To_Target = Vector3.Distance(transform.position, Monster_Target.position);

        if (Distance_To_Target >= Stopping_Radius)
        {
            if (!Is_Walking)
            {
                Is_Walking = true;
                Monster_Animator.SetBool("Is_Walking", true);
            }

            Walk_To_Target();
        }

        else
        {
            if (Is_Walking)
            {
                Is_Walking = false;
                Monster_Animator.SetBool("Is_Walking", false);
            }
        }

        if (Random.Range(1, 500) == Random.Range(1, 500))
        {
            int Random_Monster_Audio_Clip = Random.Range(0, 2);

            if (Random_Monster_Audio_Clip == 0)
            {
                AudioSource.PlayClipAtPoint(Monster_Growl_1_Clip, transform.position);
            }

            else
            {
                AudioSource.PlayClipAtPoint(Monster_Growl_2_Clip, transform.position);
            }
        }
    }

    public void Walk_To_Target()
    {
        Quaternion Monster_Rotation = Quaternion.LookRotation(Monster_Target.position - transform.position);

        Monster_Rotation.eulerAngles = new Vector3(0, Monster_Rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, Monster_Rotation, Turn_Speed * Time.deltaTime);

        Vector3 Walking_Direction = new Vector3(transform.forward.x, 0, transform.forward.z);

        Vector3 Target_Position = transform.position + Walking_Direction * Walk_Speed * Time.deltaTime;
        Target_Position.y = Monster_Y_Position;

        float Distance_To_Destination = Vector3.Distance(Target_Position, Monster_Target.position);

        if (!Check_For_Obstacle())
        {
            if (Distance_To_Destination < Stopping_Radius)
            {
                Is_Walking = false;
                Monster_Animator.SetBool("Is_Walking", false);
            }

            else
            {
                transform.position = Target_Position;
            }
        }   
    }

    public void Set_Monster_Destination(Vector3 Target_Destination)
    {
        Monster_Target.position = Target_Destination;
    }

    public bool Check_For_Obstacle()
    {
        RaycastHit Hit;

        if (Physics.Raycast(transform.position, transform.forward, out Hit, Obstacle_Avoidance_Distance))
        {
            if (Hit.collider.CompareTag("Monster_Obstacle"))
            {
                //Debug.Log("Obstacle detected: " + Hit.collider.name);
                return true;
            }
        }

        return false;
    }

    public void Pause_Monster()
    {
        Walk_Speed = 0;
    }

    public void Resume_Monster()
    {
        Walk_Speed = Original_Walk_Speed;
    }
}
