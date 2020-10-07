using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Gun : MonoBehaviour, IGun
{

    public float NextShotTime { get; set; }
    public bool CanShoot { get; set; }
    public float ShootingDelay { get => _shootingDelay; set => _shootingDelay = value; }
    public float FireRate { get => 1 / _shootingDelay; set { } }
    public float GunDamage { get => _shotDamage; private set { } }
    public float BulletSpeed { get => bulletSpeed; private set { } }

    [SerializeField] private float _shotDamage = 10f;
    [SerializeField] private float _shootingDelay = 1f;
    [SerializeField] private float bulletSpeed = 10f;

    [SerializeField]
    private Transform gunTip = null;
    [SerializeField]
    private float bulletDamageMultiplier = 1f;

    [SerializeField] private GunInfo _gunInfo= null;

    private Turret myTurret = null;

    private void Awake()
    {
        myTurret = GetComponentInParent<Turret>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CanShoot)
            Shoot();
    }


    public void Shoot()
    {

        if (NextShotTime <= Time.time && myTurret.targetEnemy != null)
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
