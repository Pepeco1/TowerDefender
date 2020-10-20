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
    public float GunProjectileDamage { get => gunDamage; protected set { } }

    [SerializeField] private float gunDamage = 10f;
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

            NextShotTime = Time.time + ShootingDelay;

            Bullet bullet = BulletPool.Instance.GetObject();
            bullet.gameObject.SetActive(true);
            bullet.transform.position = gunTip.position;
            bullet.transform.rotation = myTurret.GetLookinDir();
            bullet.targetToFollow = myTurret.targetEnemy;
            bullet.TotalDamage = gunDamage;
            bullet.speed = BulletSpeed;

        }

    }

    public string GunDamageToString()
    {
        return shotDamage.ToString();
    }

}
