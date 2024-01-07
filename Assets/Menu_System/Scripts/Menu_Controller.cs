using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Controller : MonoBehaviour
{
    [SerializeField]
    public GameManager Game_Manager;

    [SerializeField]
    public string Game_Scene;

    [SerializeField]
    public string Main_Menu_Scene;

    [SerializeField]
    public GameObject Pause_Panel;

    [SerializeField]
    public GameObject How_To_Play_Main_Menu;

    [SerializeField]
    public GameObject How_To_Play_Secondary_Menu;

    [SerializeField]
    public GameObject How_To_Play_Third_Menu;

    [SerializeField]
    public GameObject Controls_Main_Menu;

    [SerializeField]
    public GameObject Controls_Secondary_Menu;

    [SerializeField]
    public GameObject Controls_Third_Menu;

    [SerializeField]
    public GameObject Options_Main_Menu;

    [SerializeField]
    public GameObject Options_Pause_Menu;

    [SerializeField]
    public GameObject Win_Panel;

    [SerializeField]
    public GameObject Game_Over_Panel;

    [SerializeField]
    public GameObject HUD;

    [SerializeField]
    public GameObject Options_Button;

    [SerializeField]
    public AudioClip Gameplay_Music;

    [SerializeField]
    public AudioClip Menu_Music;

    [SerializeField]
    public AudioSource Game_Music;

    [SerializeField]
    public AudioClip Win_Sound;

    [SerializeField]
    public AudioClip Game_Over_Sound;

    [SerializeField]
    public bool Is_Pause_Menu_Available = false;

    [SerializeField]
    public bool Is_Game_Paused = false;

    [SerializeField]
    public bool Is_Game_Done = false;

    public void Update()
    {
        if (Is_Pause_Menu_Available)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!Is_Game_Done)
                {
                    if (Is_Game_Paused)
                    {
                        Resume_Game();
                    }

                    else
                    {
                        Pause_Game();
                    }
                }
            }
        }
    }

    public void Pause_Game()
    {
        Pause_Panel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Is_Game_Paused = true;
        Time.timeScale = 0f;
        Play_Menu_Music();
    }

    public void Resume_Game()
    {
        Pause_Panel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Is_Game_Paused = false;
        Time.timeScale = 1f;
        Play_Gameplay_Music();
    }

    public void Start_Game()
    {
        Is_Game_Done = false;
        Is_Game_Paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        HUD.SetActive(true);
        Cursor.visible = false;
        SceneManager.LoadScene(Game_Scene);
        Time.timeScale = 1f;
    }

    public void Quit_Game()
    {
        Debug.Log("You Have Quit The Game!");
        Application.Quit();
    }

    public void Open_How_To_Menu_Main()
    {
        How_To_Play_Main_Menu.SetActive(true);
        Options_Button.SetActive(false);
    }

    public void Close_How_To_Menu_Main()
    {
        How_To_Play_Main_Menu.SetActive(false);
        Options_Button.SetActive(true);
    }

    public void Open_How_To_Menu_Second()
    {
        How_To_Play_Secondary_Menu.SetActive(true);
    }

    public void Close_How_To_Menu_Second()
    {
        How_To_Play_Secondary_Menu.SetActive(false);
    }

    public void Open_How_To_Menu_Third()
    {
        How_To_Play_Third_Menu.SetActive(true);
    }

    public void Close_How_To_Menu_Third()
    {
        How_To_Play_Third_Menu.SetActive(false);
    }

    public void Open_Controls_Main()
    {
        Controls_Main_Menu.SetActive(true);
        Options_Button.SetActive(false);
    }

    public void Close_Controls_Main()
    {
        Controls_Main_Menu.SetActive(false);
        Options_Button.SetActive(true);
    }

    public void Open_Controls_Second()
    {
        Controls_Secondary_Menu.SetActive(true);
    }

    public void Close_Controls_Second()
    {
        Controls_Secondary_Menu.SetActive(false);
    }

    public void Open_Controls_Third()
    {
        Controls_Third_Menu.SetActive(true);
    }

    public void Close_Controls_Third()
    {
        Controls_Third_Menu.SetActive(false);
    }

    public void Open_Options_Main()
    {
        Options_Main_Menu.SetActive(true);
        Options_Button.SetActive(false);
    }

    public void Close_Options_Main()
    {
        Options_Main_Menu.SetActive(false);
        Options_Button.SetActive(true);
    }

    public void Return_To_Main_Menu()
    {
        Cursor.visible = true;
        SceneManager.LoadScene(Main_Menu_Scene);
    }

    public void Open_Options_Pause_Menu()
    {
        Options_Pause_Menu.SetActive(true);
    }

    public void Close_Options_Pause_Menu()
    {
        Options_Pause_Menu.SetActive(false);
    }

    public void Open_Game_Over_Panel()
    {
        Time.timeScale = 0f;
        Game_Music.Stop();
        Game_Music.PlayOneShot(Game_Over_Sound);
        Is_Game_Done = true;
        HUD.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Game_Over_Panel.SetActive(true);
    }

    public void Open_Win_Panel()
    {
        Time.timeScale = 0f;
        Game_Music.Stop();
        Game_Music.PlayOneShot(Win_Sound);
        Is_Game_Done = true;
        HUD.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Win_Panel.SetActive(true);
    }

    public void Play_Gameplay_Music()
    {
        Game_Music.Stop();
        Game_Music.clip = Gameplay_Music;
        Game_Music.Play();
    }

    public void Play_Menu_Music()
    {
        Game_Music.Stop();
        Game_Music.clip = Menu_Music;
        Game_Music.Play();
    }
}
