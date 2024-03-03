using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class DoExecutor
{

    public static void RunShortCommand(string command)
    {
        switch(command)
        {
            case "QuitGame":
                QuitGame();
                break;
            case "Fight_GameOver":
                Fight_GameOver();
                break;
            case "Fight_Win":
                break;
            case "Fight_Pause":
                Fight_Pause();
                break;
            case "Fight_RePlay":
                Fight_RePlay();
                break;
            default:break;
        }
    }

    public static void RunCommand(string command)
    {
        
        if (command == "") return;
        string[] comm = command.Split(" ");
        if (comm.Length == 0) return;
        

        switch (comm[0])
        {
            default: break;
        }
    }

    public static void GameOver()
    {
        SceneManager.LoadScene("Result");

    }

    public static void Fight_Pause()
    {
        Time.timeScale = 0;

        FightManager.instance.ShowBox();
    }

    public static void Fight_RePlay()
    {
        Time.timeScale = 1;

        FightManager.instance.HideBox();
    }

    public static void Fight_GameOver()
    {
        FightManager.instance.GameOver();
    }

    public static void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
