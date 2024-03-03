using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public class Text_GetDebugMessage : MonoBehaviour
{
    public Text text;

    void Awake()
    {
        Application.logMessageReceived += HandleLog;
    }

    void HandleLog(string message, string stackTrace, LogType tyoe)
    {
        switch (tyoe)
        {
            case LogType.Error:
                text.text += '\n'+  " <color=#FF0000>" + message + "</color>";
                break;
            case LogType.Log:
                text.text += '\n' + " " + message;
                break;
            default: break;
        }
    }

    void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
    }
}