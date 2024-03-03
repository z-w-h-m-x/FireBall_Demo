using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MIEnemy : IPlayer
{
    public TextMesh HPT;

    private void Awake() {
        type = Type.Enemy;
        HP = 3;
        EnemyHarm = 1;
        IConfig();
    }

    public override void UpdateDo()
    {
        base.UpdateDo();

        HPT.text = HP.ToString();
    }

    public virtual void IConfig(){}
    public virtual void afterInit(){}

    public void Init(Vector3 position,int id)
    {
        ID=id;
        this.transform.position = position;
        transform.DOMoveY(-4.25f,30f).SetEase(Ease.Linear);

        afterInit();
    }

    public override void WhenTriggerEnter(Type type)
    {
        base.WhenTriggerEnter(type);
        if (type == Type.land) Destroy(this.gameObject);
    }

    public override void WhenDead()
    {
        base.WhenDead();

        FightManager.instance.EnemyDeaded(ID);

        Destroy(this.gameObject);
    }

    public override void Injured(int harm)
    {
        base.Injured(harm);
        HP -= harm;
    }
}
