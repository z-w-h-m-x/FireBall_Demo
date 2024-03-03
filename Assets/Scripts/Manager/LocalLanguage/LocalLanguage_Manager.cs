using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LocalLanguage_Manager : MonobehaviourSingleton<LocalLanguage_Manager>
{
    public List<TextAsset> localLanguageFiles = new List<TextAsset>();
    public string defaultLanguage;
    public Font defaultFont;
    public bool BestFit = false;
    public int maxSize = 0;
    private List<LocalLanguage_Text_Influencer> textInfluencerList = new List<LocalLanguage_Text_Influencer>();
    private Dictionary<string, string> localLanguageData = new Dictionary<string, string>();
    private Dictionary<string, int> localLanguageFilesPoint = new Dictionary<string, int>();

    public void Init()
    {
        foreach(TextAsset _point in localLanguageFiles)//�Ա��������ļ����з���
        {
            string[] a = _point.text.ToString().Split("\r\n");
            localLanguageFilesPoint.Add(a[0], localLanguageFiles.IndexOf(_point));
            ReadLocalLanguageFileData();
        }
        
    }

    public void ChangeLocalLanguage(string key)
    {
        defaultLanguage = key;
        localLanguageData.Clear();
        localLanguageData = new Dictionary<string, string>();

        ReadLocalLanguageFileData();

        foreach(LocalLanguage_Text_Influencer _influencer in textInfluencerList)
        {
            ChangeInfluencerText(_influencer);
        }
    }

    public void Register(LocalLanguage_Text_Influencer textInfluencer)//ע���ı�������������п��ƣ�
    {
        if (textInfluencerList.Contains(textInfluencer) == true)
        {
            return;
        }
        textInfluencerList.Add(textInfluencer);
        ChangeInfluencerText(textInfluencer);
        SetFont(textInfluencer);
    }

    public void Cancel(LocalLanguage_Text_Influencer textInfluencer)//ȡ��ע�ᣨ��ֹ��������ʱ���岻���ڣ�
    {
        if (textInfluencerList.Contains(textInfluencer))
        {
            textInfluencerList.Remove(textInfluencer);
        }
    }

    private void ReadLocalLanguageFileData()//��ȡ���������ļ�
    {
        string[] lLocalLDataFile = localLanguageFiles[localLanguageFilesPoint[defaultLanguage]].text.ToString().Split("\r\n");
        for (int _point = 1; _point < lLocalLDataFile.Length; _point++)
        {
            string[] data_ = lLocalLDataFile[_point].Split("==");
            localLanguageData.Add(data_[0], data_[1]);
        }
    }

    private void ChangeInfluencerText(LocalLanguage_Text_Influencer textInfluencer)
    {
        if (localLanguageData.ContainsKey(textInfluencer.key)){
            textInfluencer.ChangeText(localLanguageData[textInfluencer.key].ToString());
        }else{
            textInfluencer.ChangeText(textInfluencer.key);
        }
    }

    private void SetFont(LocalLanguage_Text_Influencer textInfluencer)
    {
        if (defaultFont != null)
            textInfluencer.ChangeFont(defaultFont);
        if (BestFit)
            if (maxSize != 0)
                textInfluencer.ChangeBestFit(BestFit,maxSize);
    }
}
