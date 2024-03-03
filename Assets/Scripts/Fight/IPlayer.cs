using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayer : MonoBehaviour
{

    public Type type;

    public int harm;//only player

    public int HP = 999;

    public int EnemyHarm = 1;

    public int ID;

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0) WhenDead();
        UpdateDo();
    }

    public virtual void UpdateDo(){}
    public virtual void Injured(int harm){}
    public virtual void WhenDead(){} 
    public virtual void WhenTriggerEnter(Type type){}
    public virtual void WhenTriggerEnter(Collider2D other,Type type){}

    private void OnTriggerEnter2D(Collider2D other) 
    {
        IPlayer ip = other.gameObject.GetComponent<IPlayer>();

        if (ip == null) goto R;

        if (type == Type.Player) goto player;
        if (type == Type.Enemy) goto enemy;
        if (type == Type.land) goto land;

player:

        if (ip.type == Type.Enemy) ip.Injured(harm);

  goto end;
enemy:
        //if (ip.type == Type.land) {ip.Injured(EnemyHarm);FightManager.instance.EnemyArchive(ID);}

  goto end;
land:
        if (ip.type == Type.Enemy) {Injured(1);FightManager.instance.EnemyArchive(ip.ID);}
  goto end;

end:    WhenTriggerEnter(ip.type);
R:      return;
    }

    public enum Type
    {
        Player,
        Enemy,
        land,
        boundary
    }

    public enum Status
    {
        life,dead
    }
}
