using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Gun : MonoBehaviour, IGun
{

    public float NextShotTime { get; set; }
    public bool CanShoot { get; set; }
    public float GunDamage { get => _gunInfo.GunDamage; private set { } }
    public GunInfo GunInfo { get => _gunInfo; set { } }

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
            bullet.TotalDamage = _gunInfo.GunDamage;
            bullet.speed = _gunInfo.BulletSpeed;

            NextShotTime = Time.time + _gunInfo.ShootingDelay;

        }

    }


}
