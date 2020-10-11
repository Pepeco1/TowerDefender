using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{

    public bool CanShoot { get; set; }
    public float GunDamage { get => _shotDamage; private set { } }


    [SerializeField] private float _shotDamage = 10f;
    [SerializeField] protected float bulletDamageMultiplier = 1f;

    [SerializeField]
    protected Transform gunTip = null;

    [SerializeField] private GunInfo _gunInfo = null;

    protected Turret myTurret = null;

    protected virtual void Awake()
    {
        myTurret = GetComponentInParent<Turret>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (CanShoot)
            Shoot();
    }


    public abstract void Shoot();



}
