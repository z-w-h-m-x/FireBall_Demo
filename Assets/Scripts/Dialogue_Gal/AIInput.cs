using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIInput : MonoBehaviour
{
    public InputField content;
    public Button accept;

    public XingChen.XingChenAPI.WhenGetContent getAICallBack;
    public string characterID;
    public Dictionary<string, List<XingChen.XCChatMessageFormat>> messages = new();

    private void Start() {
        Disable();

        accept.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        Disable();
        XingChen.XCChatMessageFormat tmp = new();
        tmp.role = "user";
        tmp.content = content.text;
        messages[characterID].Add(tmp);
        StartCoroutine(XingChen.XingChenAPI.SendChat(characterID,messages[characterID],getAICallBack));
    }

    public void AddMessage(string message)
    {
        if (!messages.ContainsKey(characterID)) messages.Add(characterID, new());

        XingChen.XCChatMessageFormat tmp = new();
        tmp.role = "assistant";
        tmp.content = content.text;
        messages[characterID].Add(tmp);
    }

    public void Enable()
    {
        content.interactable = true;
        accept.interactable = true;
    }
    public void Disable()
    {
        content.interactable = false;
        accept.interactable = false;
    }
}
