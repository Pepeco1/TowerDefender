using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFireRateTurretStat : UIAbstractTurretStats
{
    public override string GetTextWithStats()
    {
        return initialString + " " + turretNode.InstantiatedTurret.TurretFireRate;
    }

}
