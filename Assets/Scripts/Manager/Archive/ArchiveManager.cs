using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchiveManager : MonobehaviourSingleton<ArchiveManager>
{
    
    private static bool isInit;

    public bool autoInit = false;

    public override void OnAwake()
    {
        if(autoInit) Init();
        //Debug.Log(ArchiveData.Data.archiveData.BGMVolume);
    }

    public void Init()
    {
        ArchiveData.Data.archive = new();

        ArchiveData.Do.ReadArchive();

        if (ArchiveData.Data.archive == null) throw new System.Exception("Archive Data is null!");

    }
}
    