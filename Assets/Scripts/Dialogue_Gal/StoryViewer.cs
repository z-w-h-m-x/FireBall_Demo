//Base on GalGame-Template(MIT)
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StoryViewer : MonoBehaviour
{
    public SkipAndNextSwitch sans;
    public StoryReader storyReader;
    public Text messageText;
    public Text nameText;
    public String[] messages;
    public int now = 0;
    public SystemLanguage Language;
    private void Start()
    {
        storyReader.Init();
        //storyReader = FindObjectOfType<StoryReader>();
        //switch (Application.systemLanguage)
        switch (Language)
        {
            case SystemLanguage.ChineseSimplified:
                messages = storyReader.zh_CN_str;
                break;
            case SystemLanguage.ChineseTraditional:
                messages = storyReader.zh_TW_str;
                break;
            case SystemLanguage.English:
                messages = storyReader.en_US_str;
                break;
            case SystemLanguage.Japanese:
                messages = storyReader.jp_JP_str;
                break;
            default : messages = storyReader.zh_CN_str; break;
        }

        // messages = storyReader.zh_CN_str;

        messageText.DOText(sans.NowMessage, 0.5f);
    }
}
