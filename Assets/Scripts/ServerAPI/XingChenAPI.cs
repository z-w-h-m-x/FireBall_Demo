using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArchiveData;
using System;
using UnityEngine.Networking;
using UnityEngine.Rendering;

namespace XingChen
{

    public static class XingChenAPI
    {
        public static Dictionary<string, string> characterKey = new();

        public static string host = "https://nlp.aliyuncs.com";

        public delegate void WhenGetContent(string content);

        public static IEnumerator SendChat(string characterName, List<XCChatMessageFormat> chatMessage, WhenGetContent callBack)
        {
            UnityWebRequest www = new UnityWebRequest();

            string apiPath = "/v2/api/chat/send";

            www.method = "POST";
            www.url = host + apiPath;

            www.SetRequestHeader("Expect", "");
            www.SetRequestHeader("accept", "*/*");
            www.SetRequestHeader("X-AcA-SSE", "disable");
            www.SetRequestHeader("X-AcA-DataInspection", "disable");
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("x-fag-appcode", "aca");
            www.SetRequestHeader("Authorization", "Bearer " + Data.archive.XCAPI);

            XCInput input = new();

            input.messages = chatMessage;
            input.botProfile.characterId = characterKey[characterName];

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ProtocolError 
                || www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.data);
            }

            ReturnFormat rf = JsonUtility.FromJson<ReturnFormat>(
                    www.downloadHandler.data.ToString()
            );

            if (rf != null) callBack(rf.message.content);
            else Debug.LogError("cound not found result");

            yield return null;
        }
    }

    [Serializable]
    public class XCChatMessageFormat
    {
        //public string name;
        public string role;
        public string content;
    }

    [Serializable]
    public class XCInput
    {
        [SerializeField]
        public List<XCChatMessageFormat> messages;
        public XCBotProfile botProfile = new();
        public XCContext context = new();
    }

    [Serializable]
    public class XCBotProfile
    {
        public string characterId;
    }

    [Serializable]
    public class XCContext
    {
        public bool useChatHistory = false;
    }

    [Serializable]
    public class ReturnFormat
    {
        public bool success;
        public RF_message message;
    }

    [Serializable]
    public class RF_message
    {
        public string content;
    }
}