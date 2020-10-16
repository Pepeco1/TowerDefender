using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class ShotDamageContinuous : ShotDamageType
{
    public float minDamage = 1f;
    public float maxDamage = 10f;

    public ShotDamageContinuous() { }

    public ShotDamageContinuous(float minDamage, float maxDamage)
    {
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
    }

    public override string ToString()
    {
        return "Min- " + minDamage + " Max- " + maxDamage;
    }

}
