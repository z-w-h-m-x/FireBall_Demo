using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CameraAutoSize : MonoBehaviour
{
    public Camera _camera;
 
    void Start()
    {
        float x = (float)Screen.height / (float)Screen.width;
 
        float y = 2.56f * x + 0.47f;
 
        _camera.orthographicSize = y;
    }
}