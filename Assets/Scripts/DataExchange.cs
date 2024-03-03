using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataExchange
{
    public static FightResultType fightStatus;
    public static FightManager.GameType fightGameType = FightManager.GameType.unlimited;
    public static int fightScore = 0;
    public static int fightMiss = 0;
    
    public enum FightResultType
    {
        none,win,over
    }
}
