using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLaser : Gun
{

    public float MaxDamage { get => maxDamage; private set { } }

    [SerializeField] private float maxDamage = 10f;
    [SerializeField] private float totalChargingTime = 1f;
    private float currentChargingTime = 0f;
    [SerializeField] private LineRenderer lineRenderer = null;
    [SerializeField] private ParticleSystem laserParticle = null;

    private ParticleSystem.EmissionModule emission;

    protected override void Awake()
    {
        base.Awake();
        lineRenderer.useWorldSpace = true;

        emission = laserParticle.emission;
        emission.enabled = false;
    }

    protected override void Update()
    {

        if (!AimLockedAtEnemy)
        {
            DeactivateLaser();
            return;
        }

        UpdateLaserEffects();
        base.Update();
    }

    private void DeactivateLaser()
    {
        lineRenderer.enabled = false;
        emission.enabled = false;
    }

    private void UpdateLaserEffects()
    {
        ActivateLaser();
        UpdateLineRendererPositions();
        UpdateParticlePosition();

    }

    private void UpdateLineRendererPositions()
    {
        lineRenderer.SetPosition(0, gunTip.position);
        lineRenderer.SetPosition(1, myTurret.targetEnemy.Transform.position);
    }

    private void UpdateParticlePosition()
    {
        RaycastHit hit;
        if (myTurret.CheckForHit(out hit))
        { 
            laserParticle.transform.position = hit.point;
            laserParticle.transform.LookAt(transform.position);
        }
    }

    private void ActivateLaser()
    {
        if (lineRenderer.enabled == false)
        {
            lineRenderer.enabled = true;
            emission.enabled = true;
        }
    }

    public override void Shoot()
    {

        myTurret.targetEnemy.Health.TakeDamage(shotDamage * Time.deltaTime);

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
