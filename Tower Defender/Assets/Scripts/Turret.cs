using UnityEngine;

public class Turret : MonoBehaviour
{

    public float ShootRangeDistance { get => _shootRangeDistance; set => _shootRangeDistance = value; }
    public float RotationSpeed { get => _rotationSpeed; set => _rotationSpeed = value; }
    public float ChangeTargetDistanceOffset { get => _changeTargetDistanceOffset; private set { } }
    public float TurretDamage { get => CalculateTurretDamage(); private set { } }
    public float TurretFireRate { get => CalculateTurretFireRate(); private set { } }
    public IEnemy targetEnemy { get; private set; }

    [SerializeField] private float _shootRangeDistance = 10f;
    [SerializeField] private float _rotationSpeed = 8f;
    [SerializeField] private float _changeTargetDistanceOffset = 0.3f;

    public Transform bottomOfTurret = null;

    private const int angleToLock = 20;

    private bool targetInRange = false;
    private bool aimLockedAtEnemy = false;
    private Quaternion initialRotation;

    public GunProjectile[] gunsArray;
    [SerializeField] private Transform partToRotate = null;

    void Awake()
    {

        gunsArray = GetComponentsInChildren<GunProjectile>();
        initialRotation = transform.rotation;

    }

    private void Start()
    {

        InvokeRepeating("UpdateTargetEnemy", 0f, 0.1f);

        SetGunsShootPermission();
    }

    // Update is called once per frame
    void Update()
    {


        if (targetEnemy == null)
            return;


        CheckIfTargetInRange();
        UpdateRotation();
        CheckAimLockedAtEnemy();
        SetGunsShootPermission();


    }

    public Quaternion GetLookinDir()
    {
        return partToRotate.rotation;
    }



    private void SetGunsShootPermission() 
    {

        bool gunPermission = aimLockedAtEnemy ? true : false;

        foreach(GunProjectile gun in gunsArray)
        {
            gun.CanShoot = gunPermission;
        }

    }

    private void UpdateTargetEnemy()
    {
        IEnemy possibleNewEnemy = EnemyManager.Instance.GetClosestEnemy(transform.position);

        if (possibleNewEnemy == null)
            return;

        float newEnemyDistance = Vector3.Distance(transform.position, possibleNewEnemy.Transform.position);
        float currentDistance = (targetEnemy == null || targetEnemy.GameObject.activeSelf == false) ? Mathf.Infinity : Vector3.Distance(transform.position, targetEnemy.Transform.position);

        if (newEnemyDistance + ChangeTargetDistanceOffset < currentDistance)
        {
            targetEnemy = possibleNewEnemy;
        }
    }

    private void CheckIfTargetInRange()
    {
        targetInRange = (targetEnemy.GameObject.activeSelf && Vector3.Distance(transform.position, targetEnemy.Transform.position) < ShootRangeDistance);
    }

    private void UpdateRotation()
    {

        Vector3 dir = targetEnemy.Transform.position - transform.position;
        Quaternion lookRotation = targetInRange ? Quaternion.LookRotation(dir) : initialRotation;

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
        RaycastHit hit;

        //Debug.DrawRay(partToRotate.position, partToRotate.forward * turretInfo.shootRangeDistance, Color.red);
        if (Physics.Raycast(partToRotate.position, partToRotate.forward, out hit, ShootRangeDistance))
        {

            if (hit.collider.GetComponentInParent<IEnemy>() != null)
            {
                aimLockedAtEnemy = true;
            }
        }
    }

    private void OnDrawGizmos()
    {

        UnityEditor.Handles.DrawWireDisc(partToRotate.transform.position, transform.up, ShootRangeDistance);

    }

    private float CalculateTurretDamage()
    {
        float totalDamage = 0;
        foreach (GunProjectile gun in gunsArray)
        {
            totalDamage += gun.GunDamage;
        }

        return totalDamage;
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


}
