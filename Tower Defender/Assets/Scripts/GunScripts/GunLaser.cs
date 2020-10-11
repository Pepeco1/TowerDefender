using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLaser : Gun
{
    // Return string here
    public override float GunDamage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    [SerializeField] private float totalChargingTime = 1f;
    private float currentChargingTime = 0f;
    [SerializeField] private LineRenderer lineRenderer = null;
    [SerializeField] private ShotDamageContinuous shotDamage = null;


    protected override void Awake()
    {
        base.Awake();
        lineRenderer.useWorldSpace = true;
    }

    protected override void Update()
    {

        if (!HasTargetEnemy())
        {
            ClearLineRenderer();
            return;
        }

        UpdateLineRenderer();
        base.Update();
    }

    private void ClearLineRenderer()
    {
        lineRenderer.enabled = false;
    }

    private void UpdateLineRenderer()
    {
        if (lineRenderer.enabled == false)
            lineRenderer.enabled = true;

        lineRenderer.SetPosition(0, gunTip.position);
        lineRenderer.SetPosition(1, myTurret.targetEnemy.Transform.position);
    }

    public override void Shoot()
    {

        myTurret.targetEnemy.Health.TakeDamage(shotDamage.minDamage * Time.deltaTime);

    }

    private IEnumerator ChargeLaser()
    {
        
        while(currentChargingTime < totalChargingTime)
        {
            currentChargingTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

    }


}
