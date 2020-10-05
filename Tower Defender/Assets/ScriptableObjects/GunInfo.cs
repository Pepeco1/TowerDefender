using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class GunInfo : ScriptableObject
{
    public float ShootingDelay { get => _shootingDelay; set => _shootingDelay = value; }
    public float GunDamage { get => _shotDamage; private set { } }
    public float BulletSpeed { get => bulletSpeed; private set { } }


    [SerializeField] private float _shotDamage = 10f;
    [SerializeField] private float _shootingDelay = 1f;
    [SerializeField] private float bulletSpeed = 10f;


}

