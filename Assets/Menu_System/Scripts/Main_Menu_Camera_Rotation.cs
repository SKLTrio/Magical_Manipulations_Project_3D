using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Menu_Camera_Rotation : MonoBehaviour
{
    [SerializeField]
    public float Camera_Rotation_Speed;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void Update()
    {
        transform.Rotate(Vector3.up, Camera_Rotation_Speed * Time.deltaTime);
    }
}
