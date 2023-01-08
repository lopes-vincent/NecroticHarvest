using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;
    
    public TextMeshProUGUI ZenModeText;
    public TextMeshProUGUI MuteText;

    private void Start()
    {
        ZenModeText.text = Globals.ZenMode ? "Mode timer" : "Zen mode";
        AudioListener.volume = Globals.Muted ? 0 : 1;
        MuteText.text = Globals.Muted ? "Unmute" : "Mute";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;  
    }
    
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }
    
    public void MuteToggle()
    {
        Globals.Muted = !Globals.Muted;
        MuteText.text = Globals.Muted ? "Unmute" : "Mute";
        AudioListener.volume = Globals.Muted ? 0 : 1;
    }
    
    public void Restart()
    {
        Time.timeScale = 1;
        GameIsPaused = false;
        SceneManager.LoadScene("Game");
    }

    public void SwitchZenMode()
    {
        Time.timeScale = 1;
        GameIsPaused = false;
        Globals.ZenMode = !Globals.ZenMode;
        SceneManager.LoadScene("Game");
    }

    public void HowToPlay()
    {
        Time.timeScale = 1;
        GameIsPaused = false;
        SceneManager.LoadScene("HowToPlay");
    }
}
