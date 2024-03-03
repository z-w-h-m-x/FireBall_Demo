using System.Collections;
using System.Collections.Generic;
using ArchiveData;
using UnityEngine;
using UnityEngine.UI;

public class ResultShow : MonoBehaviour
{

    public Text score;
    public Text BestScore;

    // Start is called before the first frame update
    void Start()
    {
        score.text = "本次成绩： "+DataExchange.fightScore;
        BestScore.text = "最好成绩： "+Data.archive.max;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
