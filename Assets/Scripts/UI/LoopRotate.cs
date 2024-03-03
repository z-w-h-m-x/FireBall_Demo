using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LoopRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.DOLocalRotate(new Vector3(0f,0f,360f),1,RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1,LoopType.Restart);
    }

    
}
