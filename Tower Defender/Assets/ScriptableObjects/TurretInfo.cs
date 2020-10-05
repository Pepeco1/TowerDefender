using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
[System.Serializable]
public class TurretInfo : ScriptableObject
{

    public float ShootRangeDistance { get => _shootRangeDistance; set => _shootRangeDistance = value; }
    public float RotationSpeed { get => _rotationSpeed; set => _rotationSpeed = value; }
    public float ChangeTargetDistanceOffset { get => _changeTargetDistanceOffset; private set { } }
    public Turret TurretPrefab { get => _turretPrefab; private set { } }


    [SerializeField] private float _changeTargetDistanceOffset = 0.3f;
    [SerializeField] private float _shootRangeDistance = 10f;
    [SerializeField] private float _rotationSpeed = 8f;
    [SerializeField] private Turret _turretPrefab = null;
    [SerializeField] private GunInfo[] gunsArray = null;


    public float GetDamageStat()
    {
        return CalculateTurretDamage();
    }

    private float CalculateTurretDamage()
    {
        float totalDamage = 0;
        foreach (GunInfo gun in gunsArray)
        {
            totalDamage += gun.GunDamage;
        }

        return totalDamage;
    }

}
