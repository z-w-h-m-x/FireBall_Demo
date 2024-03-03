using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

using ShortLog;
using System;


public class Loading_ANI_1 : MonoBehaviour
{
    public Sprite circle;
    public Sprite solid_circle;

    public Image image;
    public Image subImage;

    private Tween aniPlay;
    public bool clockwise = true;
    private sbyte isClockwise;

    Log log;

    private void Awake()
    {
        PlayAni();
        StartCoroutine(Init());
    }

    private void PlayAni()
    {
        if (clockwise)
            isClockwise = -1;
        else
            isClockwise =  1;
        image.sprite = circle;
        image.fillAmount = 0.036f;
        aniPlay = this.transform.DOLocalRotate(new Vector3(0f,0f,360f*isClockwise),1,RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1,LoopType.Restart);
    }

    void PlayErrorAni()
    {
        subImage.fillAmount = 0f;
        subImage.color = Color.red;
        DOTween.To( () => image.fillAmount, x => image.fillAmount = x, 1f , 0.75f).SetEase(Ease.InOutCirc);
        DOTween.To( () => subImage.fillAmount, x => subImage.fillAmount = x, 1f , 1f).SetEase(Ease.InOutCirc);
    }

    IEnumerator Init()
    {
        log=log-"Load Config";
        Config.Load();
        yield return new WaitForSeconds(1.5f);
        log=log-"Archive INIT...";
        try{ArchiveManager.instance.Init();}catch(Exception error){Debug.LogError("Catch on ArchiveManager: "+ error);ArchiveData.Data.archive = new();ArchiveData.Do.SaveArchive();}
        log=log-"LocalLangugae INIT...";
        try{LocalLanguage_Manager.instance.Init();}catch{Debug.LogError("Catch on LocalLanguageManager");PlayErrorAni();yield break;}
        log=log-"Dont have AbPackgae should be loaded";
        log=log-"ψ(｀∇´)ψ";

        if (Config.isPass)
        {
            log=log-"Config Pass";
            //aniPlay.Kill();
            subImage.fillAmount = 0f;
            DOTween.To( () => image.fillAmount, x => image.fillAmount = x, 1f , 0.75f).SetEase(Ease.InOutCirc);
            //yield return new WaitForSeconds(0.3f);
            DOTween.To( () => subImage.fillAmount, x => subImage.fillAmount = x, 1f , 1f).SetEase(Ease.InOutCirc);

            yield return new WaitForSeconds(1.2f);

            aniPlay.Kill();

            log=log-"Load Start Screen";
            yield return new WaitForSeconds(0.5f);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("StartScreen",LoadSceneMode.Single);

            while (!asyncLoad.isDone)
            {
                this.transform.localScale = new Vector3(1.46f - 1.46f * (1 - asyncLoad.progress),
                                                        1.46f - 1.46f * (1 - asyncLoad.progress),
                                                        1.46f - 1.46f * (1 - asyncLoad.progress));

                yield return null;
            }
        }
        else
        {
            Debug.LogError("Config cant pass");
            PlayErrorAni();
        }
    }
}
