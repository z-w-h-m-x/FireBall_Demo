using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonotDestoryOnLoad : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
