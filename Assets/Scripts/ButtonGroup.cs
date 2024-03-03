using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

[Serializable]
public class ButtonGroup : MonoBehaviour
{
    [SerializeField]
    public List<ButtonInfo> buttonInfos;

    public Button button;
    public LocalLanguage_Text_Influencer text;

    public bool offline = false;
    public bool isManager = false;
    public int number;
    public string doCommand;
    public string targetCommand;

    public bool canDo;

    public ButtonGroup manager;

    public void OnClick()
    {
        canDo = true;
        doCommand = targetCommand;
    }

    public void Do()
    {
        /*
        Command Format:
            scene <name>
            sub
            back
            close <half>
            open
            changeText <-text->
            run <-command->   // see DoExecutor
            command <-comands->

            ";" is end flag. it is a must
        */

        if (!canDo) return;

        canDo = false;

        string[] commands = doCommand.Split(";");//或者单数？

        if (commands.Length == 0) return;

        foreach (string command in commands)
        {
            if (command == "") break;
            string[] comm = command.Split(" ");
            if (comm.Length == 0) break;
            switch (comm[0])
            {
                case "scene":
                    if (comm.Length < 2) break;
                    Time.timeScale = 1;
                    SceneManager.LoadScene(comm[1]);
                    break;
                case "sub":
                    manager.Sub(number);
                    break;
                case "back":
                    manager.Init();
                    break;
                case "close":
                    button.interactable = false;
                    if (comm.Length < 2) 
                    {
                        text.ChangeText("");
                        break;
                    }
                    if (comm[1] == "half") break;
                    text.ChangeText("");
                    break;
                case "open":
                    button.interactable = true;
                    break;
                case "changeText":
                    if (comm.Length < 2) break;
                    text.ChangeKey(comm[1]);
                    text.Refresh();
                    break;
                case "run":
                    DoExecutor.RunShortCommand(comm[1]);
                    break;
                case "command":
                    DoExecutor.RunCommand(String.Join(" ",comm.Skip<string>(1)));
                    break;
                case "sendUp":
                    if (comm.Length < 2) break;
                    SendMessageUpwards(comm[1]);
                    break;
                case "openUrl":
                    if (comm.Length < 2) break;
                    Application.OpenURL(comm[1]);
                    break;
                default: Debug.LogError("Invalid command: " + comm[0]);break;
            }
        }
    }

    public void Sub(int number)
    {
        for ( int i = 0; i <= buttonInfos.Count - 1; i++)
        {
            ButtonGroup target = buttonInfos[i].button;
            target.doCommand = "close";
            target.canDo = true;
        }

        foreach (SubButtonInfo sbi in buttonInfos[number].subButton)
        {
            ButtonGroup target = buttonInfos[sbi.number].button;

            string a = "open";
            if (!sbi.open)
                if (sbi.closeHalf) a = "close half";
                else a = "close";

            target.doCommand = ""
                + "changeText " + sbi.text + ";"
                + a + ";";
            target.targetCommand = sbi.targetCommand;
            target.canDo = true;
        }

    }

    public void Init()
    {
        foreach (ButtonInfo bi in buttonInfos)
        {
            ButtonGroup target = bi.button;
            string a = "open";
            if (!bi.open)
                if (bi.closeHalf) a = "close half";
                else a = "close";

            target.doCommand = ""
                + a + ";"
                + "changeText " + bi.text + ";";
            target.number = buttonInfos.IndexOf(bi);
            target.targetCommand = bi.targetCommand;
            target.canDo = true;
        }
    }

    void Start()
    {
        if (isManager) 
        {
            Init();
            return;
        }

        manager = gameObject.transform.parent.GetComponent<ButtonGroup>();

        if (manager == null && !offline) { Debug.LogError("Can not found buttongroup manager"); Destroy(this); }

        button = GetComponent<Button>();

        text = this.GetComponentInChildren<LocalLanguage_Text_Influencer>();

        button.onClick.AddListener(OnClick);
    }

    private void Update()
    {
        if (!isManager) Do();
    }

}

[Serializable]
public class ButtonInfo
{
    public ButtonGroup button;
    public string text;
    public string targetCommand;
    public bool open = true;
    public bool closeHalf = false;
    [SerializeField]
    public List<SubButtonInfo> subButton;//include self
}

[Serializable]
public class SubButtonInfo
{
    public int number;
    public string text;
    public string targetCommand;
    public bool open = true;
    public bool closeHalf = false;
}