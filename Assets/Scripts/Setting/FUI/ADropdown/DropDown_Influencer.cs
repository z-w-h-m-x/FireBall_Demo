using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDown_Influencer : MonoBehaviour
{
    private DropDownManager DDManager;
    private TMP_Dropdown thisDD;
    private int lastValue;
    public string key;

    private void Start()
    {
        DDManager = GameObject.FindGameObjectWithTag("Drop Down Manager").GetComponent<DropDownManager>();
        thisDD = this.gameObject.GetComponent<TMP_Dropdown>();

        lastValue = DDManager.GetValue(key);
        thisDD.value = lastValue;
    }

    private void Update()
    {
        if(thisDD.value != lastValue)
        {
            lastValue = thisDD.value;
            DDManager.ChangeValue(key, thisDD.value);
        }    
    }

}
