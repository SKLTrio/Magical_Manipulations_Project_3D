using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio_Manager : MonoBehaviour
{
    [SerializeField]
    public Slider Volume_Slider;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        float Saved_Volume = PlayerPrefs.GetFloat("Volume", 0.5f);
        Volume_Slider.value = Saved_Volume;
        Set_Volume_Slider(Saved_Volume);
    }

    public void Set_Volume_Slider(float Volume_Amount)
    {
        AudioListener.volume = Volume_Amount;
        PlayerPrefs.SetFloat("Volume", Volume_Amount);
        PlayerPrefs.Save();
    }
}
