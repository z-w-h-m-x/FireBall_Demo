using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class Audio_WhenSceneChange : MonoBehaviour
{
    public AudioSource audioSource;

    public List<string> sceneName;

    public Dictionary<string, bool> sceneStatus = new();

    void Awake()
    {
        foreach (string name in sceneName)
        {
            sceneStatus.Add(name, false);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnLoaded;
    }

    void Update()
    {
        if (!sceneStatus.ContainsValue(true))
        {
            if (audioSource.isPlaying == false) return;
            else audioSource.Stop();
        }
        else
        {
            if (audioSource.isPlaying == true) return;
            else audioSource.Play();
        }
    }

    private void OnDestroy() 
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnLoaded;
    }

    public void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        if (sceneStatus.ContainsKey(scene.name))
        {
            sceneStatus[scene.name] = true;
        }
    }

    public void OnSceneUnLoaded(Scene scene)
    {
        if (sceneStatus.ContainsKey(scene.name))
        {
            sceneStatus[scene.name] = false;
        }
    }
}
