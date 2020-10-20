using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShotDamageProjectile : ShotDamageType
{
    public float damage = 10f;

    public override string ToString()
    {
        return damage.ToString();
    }
}
