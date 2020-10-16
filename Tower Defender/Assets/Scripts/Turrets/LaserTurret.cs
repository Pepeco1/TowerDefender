using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : Turret
{

    #region Override Methods

    protected override float CalculateTurretDamage()
    {
        if(gunsArray.Length > 1)
        {
            Debug.LogError("LaserTurret with more than 1 gun");
        }

        return gunsArray[0].ShotDamage;
    }

    public override string GetDamageStatString()
    {
        GunLaser gunLaser = (GunLaser) gunsArray[0];
        return "\n    Min- " + gunLaser.ShotDamage + " Max- " + gunLaser.MaxDamage;
    }

    #endregion

}
