using System;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public float ShootRangeDistance { get => _shootRangeDistance; set => _shootRangeDistance = value; }
    public float RotationSpeed { get => _rotationSpeed; set => _rotationSpeed = value; }
    public float ChangeTargetDistanceOffset { get => _changeTargetDistanceOffset; private set { } }
    public float TurretDamage { get => CalculateTurretDamage(); private set { } }
    public float TurretFireRate { get => CalculateTurretFireRate(); private set { } }
    public IEnemy targetEnemy { get; protected set; }
    public bool AimLockedAtEnemy { get => aimLockedAtEnemy; private set { } }

    [SerializeField] private float _shootRangeDistance = 10f;
    [SerializeField] private float _rotationSpeed = 8f;
    [SerializeField] private float _changeTargetDistanceOffset = 0.3f;


    public Transform bottomOfTurret = null;

    private const int angleToLock = 20;

    protected bool targetInRange = false;
    protected bool aimLockedAtEnemy = false;
    private Quaternion initialRotation;

    public Gun[] gunsArray;
    [SerializeField] private Transform partToRotate = null;


    #region Unity functions
    void Awake()
    {

        gunsArray = GetComponentsInChildren<Gun>();
        initialRotation = transform.rotation;

    }

    private void Start()
    {
        SetGunsShootPermission();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfTargetInRange();

        if (!targetInRange)
            UpdateTargetEnemy();

        UpdateRotation();
        CheckAimLockedAtEnemy();
        SetGunsShootPermission();

    }
    private void OnDrawGizmos()
    {

        UnityEditor.Handles.DrawWireDisc(partToRotate.transform.position, transform.up, ShootRangeDistance);

    }

    #endregion

    #region Public functions
    public Quaternion GetLookinDir()
    {
        return partToRotate.rotation;
    }

    public bool CheckForHit(out RaycastHit hit)
    {
        return Physics.Raycast(partToRotate.position, partToRotate.forward, out hit, ShootRangeDistance);
    }

    public virtual string GetDamageStatString()
    {
        return CalculateTurretDamage().ToString();
    }

    #endregion

    #region private functions

    private void SetGunsShootPermission() 
    {

        bool gunPermission = aimLockedAtEnemy ? true : false;

        foreach(Gun gun in gunsArray)
        {
            gun.CanShoot = gunPermission;
        }


    }

    private void CheckIfTargetInRange()
    {
        targetInRange = ((targetEnemy != null) && (targetEnemy.GameObject.activeSelf) && Vector3.Distance(transform.position, targetEnemy.Transform.position) < ShootRangeDistance);
    }

    private void UpdateRotation()
    {

        if (!targetInRange)
            return;

        Vector3 dir = targetEnemy.Transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        // if the angle difference is small, snap to the target
        if(Quaternion.Angle(lookRotation, partToRotate.rotation) < angleToLock)
        {
            partToRotate.rotation = lookRotation;
        }

        Vector3 rotationInEuler = Quaternion.Lerp(partToRotate.rotation, lookRotation, RotationSpeed * Time.deltaTime).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0, rotationInEuler.y, 0);
    }

    private void CheckAimLockedAtEnemy()
    {
        aimLockedAtEnemy = false;
        int hitLayers = 1 << 0; // Default layer
        RaycastHit[] hits = GetRaycastHits(hitLayers);

        //Debug.DrawRay(partToRotate.position, partToRotate.forward * turretInfo.shootRangeDistance, Color.red);
        if (hits.Length > 0)
        {
            foreach(var hit in hits)
            {
                IEnemy enemy = hit.collider.GetComponentInParent<IEnemy>();

                if (enemy != null && enemy.Equals(targetEnemy))
                {
                    aimLockedAtEnemy = true;
                }
            }
        }  
    }

    private RaycastHit[] GetRaycastHits(int hitLayers = 1 << 0)
    {
        return Physics.RaycastAll(partToRotate.position, partToRotate.forward, ShootRangeDistance, hitLayers);
    }



    private float CalculateTurretFireRate()
    {
        float fireRate = 0;
        foreach (GunProjectile gun in gunsArray)
        {
            fireRate += gun.FireRate;
        }

        return fireRate / gunsArray.Length;
    }

    #endregion

    #region Protected Methods

    protected virtual float CalculateTurretDamage()
    {
        float totalDamage = 0;
        foreach (GunProjectile gun in gunsArray)
        {
            totalDamage += gun.GunProjectileDamage;
        }

        return totalDamage;
    }

    protected virtual void UpdateTargetEnemy()
    {

        if (targetInRange)
            return;

        targetEnemy = null;
        aimLockedAtEnemy = false;

        IEnemy possibleNewEnemy = EnemyManager.Instance.GetClosestEnemy(transform.position);
        if (possibleNewEnemy == null)
            return;

        float distanceToNewEnemy = Vector3.Distance(transform.position, possibleNewEnemy.Transform.position);
        //float distanceToCurrentEnemy = (targetEnemy == null || targetEnemy.GameObject.activeSelf == false) ? Mathf.Infinity : Vector3.Distance(transform.position, targetEnemy.Transform.position);
        //if (distanceToNewEnemy + ChangeTargetDistanceOffset < distanceToCurrentEnemy)
        
        if(distanceToNewEnemy < ShootRangeDistance)
            targetEnemy = possibleNewEnemy;
        
    }

    #endregion

}
