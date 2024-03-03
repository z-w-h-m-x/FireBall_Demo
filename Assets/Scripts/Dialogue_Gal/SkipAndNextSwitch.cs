//Base on GalGame-Template(MIT)
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipAndNextSwitch : MonoBehaviour
{
    public StoryViewer Viewer;
    public CanvasGroup Skip, Next;
    public CanvasGroup DarkScreen;

    public AIInput aiInput;
    public string endCommand = "";
    public bool inAIMode = false;

    public Dictionary<string, string> id2name = new();

    public string NowMessage
    {
        get
        {
            string tmp = "";
        li:
            tmp = Viewer.messages[Viewer.now];

            if (isCommand(tmp)) { DoCommand(tmp); Viewer.now++; goto li; }

            if (isMix(tmp)) tmp = GetMix(tmp);

            aiInput.AddMessage(tmp);

            return tmp;
        }
    }

    public bool isCommand(string content)
    {
        if (content[0] == '#')
            return true;
        else return false;
    }
    public bool isMix(string content)
    {
        if (content[0] == '$')
            return true;
        else return false;
    }

    private void Start()
    {
        Skip.blocksRaycasts = true;
        Next.blocksRaycasts = false;
    }

    public void skip()
    {
        Skip.blocksRaycasts = false;
        Next.blocksRaycasts = true;

        DOTween.Clear();
        Viewer.messageText.text = NowMessage;
    }

    public void next()
    {
        if (Viewer.now < Viewer.messages.Length - 1)
        {
            Skip.blocksRaycasts = true;
            Next.blocksRaycasts = false;

            Viewer.now++;
            Viewer.messageText.text = " ";
            Viewer.messageText.DOText(NowMessage, 0.5f);
        }
        else
        {
            StartCoroutine(LoadNextScene());
        }
    }

    private void Update()
    {
        if (FindObjectOfType<PrepareScreen>().isRunning)
        {
            if (inAIMode) return;

            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            {
                if (Viewer.now < Viewer.messages.Length - 1)
                {
                    Viewer.now += 1;
                    Viewer.messageText.text = NowMessage;
                }
                else
                {
                    FindObjectOfType<PrepareScreen>().isRunning = false;
                    StartCoroutine(LoadNextScene());
                }
            }

            if (Viewer.messageText.text == NowMessage)
            {
                skip();
            }
        }
    }

    public void SetSNAct(bool act)
    {
        Skip.gameObject.SetActive(act);
        Next.gameObject.SetActive(act);
    }

    IEnumerator LoadNextScene()
    {
        DarkScreen.DOFade(1f, 1f).SetEase(Ease.OutSine);
        yield return new WaitForSeconds(1f);
        //SceneManager.LoadSceneAsync(Application.loadedLevel + 1);

        EndDo();
    }

    public void EndDo()
    {
        DoCommand(endCommand);
    }

    public string GetMix(string tmp)
    {
        return tmp;
    }

    public void AICallBack(string content)
    {
        SetSNAct(true);
        inAIMode = false;

        aiInput.AddMessage(content);
        Viewer.messageText.DOText(content, 0.5f);
    }

    public void DoCommand(string command)
    {
        if (command == "") return;
        string[] comm = command.Split(" ");
        if (comm.Length == 0) return;
        switch (comm[0])
        {
            case "#scene":
                if (comm.Length < 2) break;
                Time.timeScale = 1;
                SceneManager.LoadScene(comm[1].ToString());
                break;
            case "#changeCharacter":
                if (comm.Length < 2) break;
                aiInput.characterID = comm[1];
                Viewer.nameText.text = comm[2];
                id2name.Add(comm[1], comm[2]);
                break;
            case "#CC":
                if (comm.Length < 2) break;
                aiInput.characterID = comm[1];
                Viewer.nameText.text = comm[2];
                id2name.Add(comm[1], comm[2]);
                break;
            case "#cc":
                if (comm.Length < 2) break;
                aiInput.characterID = comm[1];
                Viewer.nameText.text = comm[2];
                if (!id2name.ContainsKey(comm[1])) id2name.Add(comm[1], comm[2]);
                else id2name[comm[1]] = comm[2];
                break;
            case "#AI":
                SetSNAct(false);
                inAIMode = true;
                aiInput.Enable();
                aiInput.getAICallBack = AICallBack;
                break;
            case "#run":
                DoExecutor.RunShortCommand(comm[1]);
                break;
            case "#sendUp":
                if (comm.Length < 2) break;
                SendMessageUpwards(comm[1]);
                break;
            case "#SetEnd":
                if (comm.Length < 2) break;
                endCommand = comm[1] + " " + comm[2];
                break;
            default: Debug.LogError("Invalid command: " + comm[0]); break;
        }

    }
}
