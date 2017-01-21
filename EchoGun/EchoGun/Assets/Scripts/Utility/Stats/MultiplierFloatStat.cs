using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MultiplierFloatStat : Stat<float>
{

    public MultiplierFloatStat(float baseValue) : base(baseValue) { }

    protected override float Recalculate()
    {
        float result = baseValue;
        foreach (float modifier in modifiers)
        {
            result *= modifier;
        }
        return result;
    }
}
