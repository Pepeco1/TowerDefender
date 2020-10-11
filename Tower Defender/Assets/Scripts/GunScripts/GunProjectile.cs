using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GunProjectile : Gun, IGun
{

    public float NextShotTime { get; set; }
    public float ShootingDelay { get => _shootingDelay; set => _shootingDelay = value; }
    public float FireRate { get => 1 / _shootingDelay; set { } }
    public float BulletSpeed { get => bulletSpeed; private set { } }

    public override float GunDamage { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    [SerializeField] private float _shootingDelay = 1f;
    [SerializeField] private float bulletSpeed = 10f;

    protected override void Awake()
    {
        base.Awake();
        myTurret = GetComponentInParent<Turret>();
    }

    public override void Shoot()
    {

        if (NextShotTime <= Time.time && HasTargetEnemy())
        {

            Bullet bullet = BulletPool.Instance.GetObject();
            bullet.gameObject.SetActive(true);
            bullet.transform.position = gunTip.position;
            bullet.transform.rotation = myTurret.GetLookinDir();
            bullet.targetToFollow = myTurret.targetEnemy;
            bullet.TotalDamage = GunDamage;
            bullet.speed = BulletSpeed;

            NextShotTime = Time.time + ShootingDelay;

        }

    }

}
