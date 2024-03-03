using System;
using System.Collections;
using System.Collections.Generic;
using ArchiveData;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class FightManager : MonobehaviourSingleton<FightManager>
{
    public TextMesh HPCount;
    public TextMesh score;

    public List<int> EnemyID;

    public GameType gameType = GameType.unlimited;

    public string gameScreenID;

    public int enemyCount;
    public int enemyKilledCount = 0;
    public int enemyArchiveCount = 0;
    public int landHP;

    public List<Vector3> track;

    public GameObject EC01;
    public GameObject EnemyGroup;
    public GameObject PauseBox;

    public ButtonGroup bg = new();

    public bool isPlaying = false;

    private bool stopTimer = false;

    public override void OnAwake()
    {
        base.OnAwake();

        StartCoroutine(PlaceEnemy());

        DataExchange.fightStatus = DataExchange.FightResultType.none;
    }

    IEnumerator PlaceEnemy()
    {
        float dt = 0f;

        int commandCount = 0;

        for (;;)
        {
            dt += Time.deltaTime;

            if (gameType == GameType.unlimited) goto unlimited;
            if (gameType == GameType.story)     goto story;

        unlimited:

            if (dt < 5f ) goto e;

            dt %= 5;

            List<int> sta = new();

            int tmp = UnityEngine.Random.Range(1,6);

            for (int i = 1; i <= tmp; i++)
            {
                int rt = UnityEngine.Random.Range(1,6);

                if (sta.Contains(rt)) goto p1;

                sta.Add(rt);
                GameObject clone = Instantiate(EC01) as GameObject;
                clone.transform.parent = EnemyGroup.gameObject.transform;

                clone.GetComponent<MIEnemy>().Init(track[rt-1],EnemyID.Count);
                EnemyID.Add(EnemyID.Count);

                p1: isPlaying = true;
            }

            goto e;

        story:

            goto e;
        
        e:
            if (stopTimer) {isPlaying = false;break;}

            yield return null;
        }

        yield return null;
    }

    public void GameOver()
    {
        DataExchange.fightStatus = DataExchange.FightResultType.over;

        DataExchange.fightScore = enemyKilledCount;
        DataExchange.fightMiss = enemyArchiveCount;

        if (gameType == GameType.unlimited)
        {
            if (Data.archive.max <= enemyKilledCount)
                Data.archive.max = enemyKilledCount;
        }

        stopTimer = true;
        bg = new();
        bg.doCommand = "scene Result;";
        bg.canDo = true;
        bg.Do();
    }

    public void Win()
    {
        Debug.Log("WIN");
        stopTimer = true;
        DataExchange.fightStatus = DataExchange.FightResultType.win;
        bg = new();
        bg.doCommand = "scene Result;";
        bg.canDo = true;
        bg.Do();
    }

    public void ShowBox()
    {
        PauseBox.SetActive(true);
    }

    public void HideBox()
    {
        PauseBox.SetActive(false);
    }

    void Update()
    {
        if (EnemyID.Count == 0 && isPlaying && gameType == GameType.story) Win();

        HPCount.text = "HP: " + landHP.ToString() + "/10";
        score.text = enemyKilledCount.ToString();
    }

    public void EnemyDeaded(int id)
    {
        if (EnemyID.Contains(id)) EnemyID.Remove(id);

        enemyKilledCount++;
    }

    public void EnemyArchive(int id)
    {
        EnemyDeaded(id);
        enemyArchiveCount++;
    }

    public enum GameType
    {
        unlimited,story
    }
}
