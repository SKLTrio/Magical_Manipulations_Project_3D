using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager Instance { get; private set; }

    [SerializeField]
    public Texture2D Cursor_Texture;

    void Start()
    {
        Cursor.SetCursor(Cursor_Texture, Vector2.zero, CursorMode.ForceSoftware);
    }

    void Update()
    {
        
    }
}
