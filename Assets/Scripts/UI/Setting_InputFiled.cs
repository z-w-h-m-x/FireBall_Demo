using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_InputFiled : MonoBehaviour
{
    public Type type;

    public InputField inputField;

    void Start()
    {
        inputField.onValueChanged.AddListener(OnChange);
    }

    public void OnChange(string content)
    {
        ArchiveData.Data.archive.XCAPI = content;
    }

    public enum Type
    {
        XingChenAPI = 1
    }
}
