using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandCount : IPlayer
{
    public bool isCheck = false;
    public Vector3 InitP;
    private void Awake() {
        type = Type.land;
        HP = 10;
        InitP = transform.position;
    }

    public override void UpdateDo()
    {
        base.UpdateDo();
        transform.position = InitP;
        if (isCheck)
            FightManager.instance.landHP = HP;
    }

    public override void Injured(int harm)
    {
        base.Injured(harm);

        if (isCheck) HP--;
    }

    public override void WhenDead()
    {
        FightManager.instance.GameOver();
    }
}
