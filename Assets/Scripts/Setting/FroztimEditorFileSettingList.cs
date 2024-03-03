using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class FileSettingList
{
    /// <summary>
    /// 启动时文件设置选项（信息设置）
    /// </summary>
    [SerializeField]
    public List<FSList> settingsList = new List<FSList>();
    
}

[SerializeField]
public class FSList
{
    public string key;//名称key值
    public string key2;
    public string ListID;//该设置对应ID，通常为变量
    public int type;//看参数枚举吧。。。
    public List<FSListMin> sonDataList = new List<FSListMin>();
}
[SerializeField]
public class FSListMin
{
    public string key;//名称key值
    public string key2;
    public string ListID;//该设置对应ID，通常为变量
    public FF.Setting.EFSLE type;//看参数枚举吧。。。
}

namespace FF.Setting 
{
    public enum EFSLE : int
    {
        Item = 1,//分类/项
        BoolType = 2,
        InputBox = 3,
    }
}