using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using EFileSettingsEnum = FF.Setting.EFSLE;

//注释.。。我这个屑就用中文了
//MS = map settings (((
public class FES_MS_Control : MonoBehaviour
{

    //public int increaseHight;//当滑动条值为一时增加的高度
    public float distance_ME_X = 30;//放置选项时X轴偏移，下同理
    public float distance_ME_Y = 30;


    public string fileSettingList;//设置项目
    public TextAsset fileSettingListFile;
    public ListDataLoadingMode listDataLoadingMode;
    public List<FSList> fSLMatrix = new List<FSList>();//设置数据列表

    public Transform fEControl;

    //信息UI预制件
    public GameObject fSLItem;
    public GameObject fSLBoolType;
    public GameObject fSLInputBox;

    public GameObject fileSettingsObject;

    //private RectTransform rectTransform;
    //private float ratio;//这个是储存滑动条的值

    private void Awake()
    {
        //rectTransform = gameObject.GetComponent<RectTransform>();
        LoadFileSettingList();
        Debug.Log("Json(fileSettingList)LoadFinished");
        Debug.Log("\"Place List Objects\" running");
        PlaceListObjects();
        Debug.Log("\"Place List Objects\" Finished ");
    }
    
    /*public void FES_MS_NewRatio(float r)//刷新位置
    {
        ratio = r;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x,increaseHight * ratio);
    }*/

    public void LoadFileSettingList()
    {
        if (listDataLoadingMode == ListDataLoadingMode.variable){
        fSLMatrix = JsonConvert.DeserializeObject<List<FSList>>(fileSettingList);}
        else if(listDataLoadingMode == ListDataLoadingMode.file){
            fSLMatrix = JsonConvert.DeserializeObject<List<FSList>>(fileSettingListFile.text.ToString());
        }
    }

    public void PlaceListObjects()//放置
    {
        float pointX = 0;
        float pointY = -30;
        foreach (FSList lists in fSLMatrix)
        {
            int th = fSLMatrix.IndexOf(lists);
            switch (fSLMatrix[th].type)
            {
                case (int)EFileSettingsEnum.Item:

                    GameObject pointObj = CloneObj(fSLItem, pointX, pointY);
                    pointObj.GetComponent<FES_MS_text_Item>().Change(fSLMatrix[th].key,fSLMatrix[th].ListID);

                    pointX += distance_ME_X;
                    pointY -= distance_ME_Y;

                    foreach (FSListMin point_ in fSLMatrix[th].sonDataList)
                    {
                        switch (point_.type)
                        {
                            case EFileSettingsEnum.InputBox:
                                CreateInputBox(point_,pointX,pointY);
                                break;
                            case EFileSettingsEnum.BoolType:
                                break;
                            default:
                                Debug.LogError("Not Found FESLID is" + point_.type);
                                break;
                        }
                        pointY -= distance_ME_X;
                    }
                    pointX -= distance_ME_X;
                    break;
                case (int)EFileSettingsEnum.InputBox:
                    CreateInputBox(fSLMatrix[th],pointX,pointY);
                    pointY -= distance_ME_X;
                    break;
                case (int)EFileSettingsEnum.BoolType:
                    pointY -= distance_ME_X;
                    break;
                default:
                    Debug.LogError("Not Found FESLID is" + fSLMatrix[th].type );
                    break;
            }
        }
        //increaseHight += ((int)Math.Ceiling(pointY));
        //GameObject.Find("Scrollbar").GetComponent<FES_MS_Scrollbar>().ChangeSize(0 - pointY);
    }

    private GameObject CreateInputBox(FSList data,float x,float y)
    {
        GameObject _point = CloneObj(fSLInputBox, x, y);
        FES_MS_IF IB = _point.GetComponent<FES_MS_IF>();

        IB.Initialization(data.key,data.key2,data.ListID);

        return _point;
    }
    private GameObject CreateInputBox(FSListMin data, float x, float y)
    {
        FSList fs = new()
        {
            key = data.key,
            key2 = data.key2,
            ListID = data.ListID,
            type = (int)data.type
        };

        return CreateInputBox(fs,x,y);
    }

    private GameObject CloneObj(GameObject gameObject_,float x,float y)
    {
        GameObject pointObj = GameObject.Instantiate(gameObject_, fEControl);
        //RectTransform pointRectTransform = pointObj.GetComponent<RectTransform>();

        //设置创建的物体
        pointObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        pointObj.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        pointObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

        return pointObj;
    }

    public enum ListDataLoadingMode{
        variable=1,file=2
    }

}
