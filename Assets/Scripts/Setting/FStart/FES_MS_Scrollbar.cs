using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FES_MS_Scrollbar : MonoBehaviour
{

    private Scrollbar scrollbar ;

    private void Start()
    {
        scrollbar = gameObject.GetComponent<Scrollbar>();
    }

    public void ChangeSize(float _value)
    {
        this.Start();
        if (_value <= Screen.height)
        {
            scrollbar.size = 1;
        }else if(_value > Screen.height)
        {
            scrollbar.size = 1 - Mathf.Log10(_value / Screen.height);
        }
    }

    public void ratioChanged()
    {
        GameObject.Find("FileSettings").SendMessage("FES_MS_NewRatio",scrollbar.value);
    }
}
