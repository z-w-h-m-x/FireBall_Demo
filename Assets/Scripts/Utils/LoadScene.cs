using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName;

    public void Click(string iSceneName)
    {
        if (iSceneName == "")
            Debug.LogError("Please enter scene name");
        else
            iLoadScene(iSceneName);
    }

    public void iLoadScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
