using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LLTextComponentType = LocalLanguage_Text_Influencer.LLTextComponentType;

public class FES_MS_IF : MonoBehaviour
{
    IBData iBData;
    string listIDData;

    private void Awake()
    {

    }

    private void Start()
    {

    }

    public void ChangeData(string data)
    {

    }

    public void Initialization()//初始化信息设置
    {
        Start();
        listIDData = iBData.listID;
        gameObject.GetComponent<RectTransform>().Find("in").gameObject.GetComponent<RectTransform>().Find("Placeholder").GetComponent<LocalLanguage_Text_Influencer>().key = iBData.placeholder;
        gameObject.GetComponent<RectTransform>().Find("tip").GetComponent<LocalLanguage_Text_Influencer>().ChangeKey(iBData.title);

    }
    public void Initialization(IBData data)
    {
        iBData = data;

        Initialization();
    }
    public void Initialization(string title,string placeholder,string listID)
    {
        iBData.title = title;
        iBData.placeholder = placeholder;
        iBData.listID = listID;

        Initialization();
    }

    public struct IBData
    {
        public string title;
        public string placeholder;
        public string listID;
    }
}
