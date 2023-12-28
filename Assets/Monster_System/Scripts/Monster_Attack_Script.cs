using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster_Attack_Script : MonoBehaviour
{
    [SerializeField]
    public float Monster_Attack_Strength;

    [SerializeField]
    public float Monster_Attack_Distance;

    [SerializeField]
    public float Attack_Cooldown_Speed;

    [SerializeField]
    public Animator Monster_Animator;

    [HideInInspector]
    public bool Can_Attack;

    [HideInInspector]
    public Player_Health_Script Player_Health;

    [HideInInspector]
    public Mana_Crystal_Health_Script Mana_Crystal_Health;

    [SerializeField]
    public Monster_Movement_Script Monster_Move_Script;

    [HideInInspector]
    public Transform Attack_Target;

    [HideInInspector]
    public string Health_Target;

    private void Start()
    {
        Player_Health = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Health_Script>();
        Mana_Crystal_Health = GameObject.FindGameObjectWithTag("Mana_Crystal").GetComponent<Mana_Crystal_Health_Script>();

        Can_Attack = true;

        if (Monster_Move_Script.Monster_Target == Monster_Move_Script.Player_Object)
        {
            Attack_Target = Monster_Move_Script.Player_Object;
            Health_Target = "Player_Object";
        }

        else
        {
            Attack_Target = Monster_Move_Script.Mana_Crytal_Object;
        }
    }

    private void Update()
    {
        float Distance_To_Target = Vector3.Distance(transform.position, Attack_Target.transform.position);

        if (Distance_To_Target <= Monster_Attack_Distance && Can_Attack)
        {
            Attack();
            StartCoroutine(Cooldown_Period());
        }
    }
    public void Attack()
    {
        if (Health_Target == "Player_Object")
        {
            Monster_Animator.SetBool("Is_Attacking", true);
            Player_Health.Take_Damage(Monster_Attack_Strength);
        }

        else
        {
            Monster_Animator.SetBool("Is_Attacking", true);
            Mana_Crystal_Health.Take_Damage(Monster_Attack_Strength);
        }
    }

    public IEnumerator Cooldown_Period()
    {
        Can_Attack = false;
        Monster_Animator.SetBool("Is_Attacking", false);
        yield return new WaitForSeconds(Attack_Cooldown_Speed);
        Can_Attack = true;
    }

}
