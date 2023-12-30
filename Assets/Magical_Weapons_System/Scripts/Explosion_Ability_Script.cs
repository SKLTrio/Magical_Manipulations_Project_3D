using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Explosion_Ability_Script : MonoBehaviour
{
    [SerializeField]
    public ParticleSystem Explosion_PS;

    [SerializeField]
    public float Explosion_Cooldown_Period;

    public void On_Use(InputAction.CallbackContext Context)
    {
        Start_Explosion();
    }

    public void Start_Explosion()
    {
        Debug.Log("BOOM");
    }
}
