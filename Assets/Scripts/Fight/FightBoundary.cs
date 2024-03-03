using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBoundary : IPlayer
{
    private void Awake() {
        type = Type.boundary;
        HP = 9999999;
    }
}
