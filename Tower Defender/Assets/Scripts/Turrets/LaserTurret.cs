using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : Turret
{

    #region Override Methods

    public override string GetDamageStatString()
    {
        GunLaser gunLaser = (GunLaser) gunsArray[0];
        return "\n    Min- " + gunLaser.ShotDamage + " Max- " + gunLaser.MaxDamage;
    }

    protected override float CalculateTurretDamage()
    {
        if(gunsArray.Length > 1)
        {
            Debug.LogError("LaserTurret with more than 1 gun");
        }

        return gunsArray[0].ShotDamage;
    }

    protected override void UpdateTargetEnemy()
    {

        if (targetInRange)
            return;

        targetEnemy = null;
        aimLockedAtEnemy = false;

        IEnemy possibleNewEnemy = EnemyManager.Instance.GetFarthestEnemyInRange(transform.position, ShootRangeDistance);
        if (possibleNewEnemy == null)
            return;

        float distanceToNewEnemy = Vector3.Distance(transform.position, possibleNewEnemy.Transform.position);
        //float distanceToCurrentEnemy = (targetEnemy == null || targetEnemy.GameObject.activeSelf == false) ? Mathf.Infinity : Vector3.Distance(transform.position, targetEnemy.Transform.position);
        //if (distanceToNewEnemy + ChangeTargetDistanceOffset < distanceToCurrentEnemy)


        targetEnemy = possibleNewEnemy;

    }

    #endregion

}
