using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownManager : MonoBehaviour
{
    public Dictionary<string, int> dropDownData = new Dictionary<string, int>();

    public void AddData(string _key,int _value)
    {
        dropDownData.Add(_key, _value);
    }

    public int GetValue(string _key)
    {
        if (!dropDownData.ContainsKey(_key))
        {
            AddData(_key, -1);
        }
        return dropDownData[_key];
    }

    public void ChangeValue(string _key,int _value)
    {
        if (!dropDownData.ContainsKey(_key))
        {
            AddData(_key, -1);
        }
        dropDownData[_key] = _value;
    }
}
