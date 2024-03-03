using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APlayerControl : MonoBehaviour
{
    public bool isSingle = false;
    
    public bool isBGM = false;

    private bool syncWSC = false;

    void Update()
    {
        if (syncWSC && gameObject.GetComponent<AudioSource>().isPlaying) return;
        if (isBGM && isSingle) this.gameObject.GetComponent<AudioSource>().volume = ArchiveData.Data.archive.BGMVolume;
    }
}
