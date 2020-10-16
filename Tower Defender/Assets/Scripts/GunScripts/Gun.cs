using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{

    public bool CanShoot { get; set; }
    protected bool AimLockedAtEnemy { get => myTurret.AimLockedAtEnemy; private set { } }
    public float ShotDamage { get => shotDamage; protected set { } }

    [SerializeField] protected float shotDamage = 10f;
    [SerializeField] protected float bulletDamageMultiplier = 1f;



    [SerializeField] protected Transform gunTip = null;
    protected Turret myTurret = null;



    #region Unity's functions

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


    #endregion
    #region Public functions

    public abstract void Shoot();

    #endregion
    #region Private Functions
    #endregion
    #region Protected Functions
    
    protected bool HasTargetEnemy()
    {
        return myTurret.targetEnemy != null;
    }

    #endregion






}
