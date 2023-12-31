using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Explosion_Ability_Script : MonoBehaviour
{
    [SerializeField]
    public ParticleSystem Explosion_PS;

    [SerializeField]
    public float Explosion_Cooldown_Period;

    [SerializeField]
    public BoxCollider Particle_Collider;

    [SerializeField]
    public GameObject Player_Camera_Container;

    [SerializeField]
    public float Screen_Shake_Duration;

    [SerializeField]
    public float Screen_Shake_Amount;

    [HideInInspector]
    public float Last_Explosion;

    [HideInInspector]
    public Vector3 Original_Screen_Position;

    [HideInInspector]
    public bool Can_Shake;

    public void Start()
    {
        Original_Screen_Position = Player_Camera_Container.transform.position;
    }


    public void Update()
    {
        if (Mouse.current.rightButton.isPressed && Time.time >= Last_Explosion + Explosion_Cooldown_Period)
        {
            Start_Explosion();
            Last_Explosion = Time.time;
        }
    }

    public void Start_Explosion()
    {
        Debug.Log("BOOM");
        Explosion_PS.Play();
        StartCoroutine("On_Off_Collider");
    }

    public IEnumerator On_Off_Collider()
    {
        yield return new WaitForSeconds(0.25f);

        Particle_Collider.enabled = true;

        yield return new WaitForSeconds(0.25f);
        
        Particle_Collider.enabled = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster_Health_Script Health_Script = other.GetComponent<Monster_Health_Script>();
            Health_Script.Monster_Health = 0f;
        }
    }
}
