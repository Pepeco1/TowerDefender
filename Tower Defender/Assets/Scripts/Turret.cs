using UnityEngine;

public class Turret : MonoBehaviour
{
    public IEnemy targetEnemy { get; private set; }

    private const int angleToLock = 20;
    [SerializeField] private float changeTargetDistanceOffset = 0.3f;

    private bool targetInRange = false;
    private bool aimLockedAtEnemy = false;

    [SerializeField] private float shootRangeDistance = 10f;

    [SerializeField] private float rotationSpeed = 8f;
    private Quaternion initialRotation;

    private Gun[] gunsArray;
    [SerializeField] private Transform partToRotate = null;

    void Awake()
    {

        gunsArray = GetComponentsInChildren<Gun>();
        initialRotation = transform.rotation;

    }

    private void Start()
    {

        InvokeRepeating("UpdateTargetEnemy", 0f, 0.05f);

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

        foreach(Gun gun in gunsArray)
        {
            gun.CanShoot = gunPermission;
        }

    }

    private void UpdateTargetEnemy()
    {
        IEnemy possibleNewEnemy = EnemyManager.Instance.GetClosestEnemy(transform.position);

        float newEnemyDistance = Vector3.Distance(transform.position, possibleNewEnemy.Transform.position);
        float currentDistance = (targetEnemy == null || targetEnemy.GameObject.activeSelf == false) ? Mathf.Infinity : Vector3.Distance(transform.position, targetEnemy.Transform.position);

        if (newEnemyDistance + changeTargetDistanceOffset < currentDistance)
        {
            targetEnemy = possibleNewEnemy;
        }
    }

    private void CheckIfTargetInRange()
    {
        targetInRange = (targetEnemy.GameObject.activeSelf && Vector3.Distance(transform.position, targetEnemy.Transform.position) < shootRangeDistance);
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

        Vector3 rotationInEuler = Quaternion.Lerp(partToRotate.rotation, lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0, rotationInEuler.y, 0);
    }

    private void CheckAimLockedAtEnemy()
    {
        aimLockedAtEnemy = false;
        RaycastHit hit;

        //Debug.DrawRay(partToRotate.position, partToRotate.forward * shootRangeDistance, Color.red);
        if (Physics.Raycast(partToRotate.position, partToRotate.forward, out hit, shootRangeDistance))
        {

            if (hit.collider.GetComponentInParent<IEnemy>() != null)
            {
                aimLockedAtEnemy = true;
            }
        }
    }

    private void OnDrawGizmos()
    {

        UnityEditor.Handles.DrawWireDisc(partToRotate.transform.position, transform.up, shootRangeDistance);

    }

}
