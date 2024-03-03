using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FES_MS_IF_text : MonoBehaviour
{
    public LLTextComponentType thisLLTCT;
    private InputField tText;
    private TMP_InputField tTextTMP;

    private void Awake()
    {
        if (GetComponent<InputField>() != null)
        {
            thisLLTCT = LLTextComponentType.Legacy;
            tText = this.GetComponent<InputField>();
        }
        else if(GetComponent<TMP_InputField>() != null)
        {
            thisLLTCT = LLTextComponentType.TMP;
            tTextTMP = this.GetComponent<TMP_InputField>();
        }
        else
        {
            Debug.LogError("GameObject : " + this.gameObject + " : Not Found Component Which About Text");
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ChangeText(string text)
    {
        switch (thisLLTCT){
            case LLTextComponentType.Legacy:
                tText.text = text;
                break;
            case LLTextComponentType.TMP:
                tTextTMP.text = text;
                break;
        }

    }

    public enum LLTextComponentType
    {
        Legacy = 1,
        TMP = 2
    }

}
