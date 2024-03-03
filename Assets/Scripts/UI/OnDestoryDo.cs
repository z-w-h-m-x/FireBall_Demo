using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArchiveData;

public class OnDestoryDo : MonoBehaviour
{
    public Type type;

    void OnDestroy()
    {
        switch(type)
        {
            case Type.SaveArchive:
                Do.SaveArchive();
                break;
            default: break;
        }
    }

    public enum Type
    {
        SaveArchive = 1
    }
}
