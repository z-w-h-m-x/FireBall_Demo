using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTargetFrameRate : MonobehaviourSingleton<SetTargetFrameRate>
{
    public bool noLimitInTheEditor = true;
    public override void OnAwake()
    {

        //QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = Screen.currentResolution.refreshRate * 2;
        
#if UNITY_EDITOR
        if (noLimitInTheEditor) Application.targetFrameRate = -1;
#endif

    }
}
