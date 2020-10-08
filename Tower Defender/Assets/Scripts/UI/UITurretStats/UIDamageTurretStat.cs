using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDamageTurretStat : UIAbstractTurretStats
{
    public override string GetTextWithStats()
    {
        return initialString + " " + turretNode.InstantiatedTurret.TurretDamage;
    }

}
