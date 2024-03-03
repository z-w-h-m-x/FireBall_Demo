using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FES_MS_text_Item : MonoBehaviour
{
    public string key;
    public string ListID;

    private void Awake()
    {
    }

    public void Change(string _key,string _ListID)
    {
        key = _key;
        ListID = _ListID;
        this.gameObject.GetComponent<LocalLanguage_Text_Influencer>().ChangeKey(_key);
        this.gameObject.GetComponent<LocalLanguage_Text_Influencer>().Refresh();
    }

    public void Destroy()
    {
        GameObject.Destroy(this.gameObject);
    }
}
