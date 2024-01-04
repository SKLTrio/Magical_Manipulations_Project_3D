using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placed_Shield_Script : MonoBehaviour
{
    [SerializeField]
    public float Time_Before_Destruction;

    public void Start()
    {
        StartCoroutine("Destroy_Shield");
    }

    public IEnumerator Destroy_Shield()
    {
        yield return new WaitForSeconds(Time_Before_Destruction);
        Destroy(gameObject);
    }
}
