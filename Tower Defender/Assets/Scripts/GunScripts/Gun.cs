using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Gun : MonoBehaviour, IGun
{

    public float ShootingDelay { get => _shootingDelay; set => _shootingDelay = value; }
    public float NextShotTime { get; set; }
    public bool CanShoot { get; set; }

    [SerializeField]
    private Transform gunTip = null;
    [SerializeField]
    private float _shootingDelay = 1f;
    [SerializeField]
    private float bulletDamageMultiplier = 1f;
    [SerializeField]
    private float bulletSpeed = 10f;

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
            bullet.damageMultiplier = bulletDamageMultiplier;
            bullet.speed = bulletSpeed;

            NextShotTime = Time.time + ShootingDelay;

        }

    }


}
