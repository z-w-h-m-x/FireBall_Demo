using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XingChen;

[Serializable]
public class ChatTestAPI : MonoBehaviour
{
    public List<CharacterKeyFormat> characterKey;
    public List<XCChatMessageFormat> message;

    // Start is called before the first frame update
    void Start()
    {
        Dictionary<string, string> tmp=new();
        foreach (CharacterKeyFormat tp in characterKey)
        {
            tmp.Add(tp.name,tp.key);
        }

        XingChenAPI.characterKey = tmp;

        StartCoroutine(XingChenAPI.SendChat("lry",message,(string a) => {}));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Serializable]
    public class CharacterKeyFormat
    {
        public string name;
        public string key;
    }
}
