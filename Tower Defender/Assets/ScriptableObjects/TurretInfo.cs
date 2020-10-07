using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
[System.Serializable]
public class TurretInfo : ScriptableObject
{


    public Turret TurretPrefab { get => _turretPrefab; private set { } }



    [SerializeField] private Turret _turretPrefab = null;
    [SerializeField] private GunInfo[] gunsArray = null;





}
