using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSettingsManager : MonobehaviourSingleton<LanguageSettingsManager>
{
    public LocalLanguage_Manager localLanguageTextManager;

    public void ChangeLocalLanguage(int cao)
    {
        switch (cao)
        {
            case 0://English(en)
                localLanguageTextManager.defaultLanguage = "en";
                break;
            case 1://Chinese(zh_cn)
                localLanguageTextManager.defaultLanguage = "zh_cn";
                break;
        }
        localLanguageTextManager.ChangeLocalLanguage(localLanguageTextManager.defaultLanguage);
    }
}
