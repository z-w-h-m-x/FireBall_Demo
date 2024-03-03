using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using ArchiveData;

public class Setting_Slider : MonoBehaviour
{
    public Slider slider;
    public Type type;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = Data.archive.BGMVolume;
        slider.onValueChanged.AddListener(OnChange);
    }

    public void OnChange(float value)
    {
        Data.archive.BGMVolume = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum Type
    {
        BGM = 1
    }
}
