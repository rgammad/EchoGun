﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class which simplifies the tracking of stats, by adding a system through which modifiers can be added and removed.
/// </summary>
/// <typeparam name="T"></typeparam>
[System.Serializable]
public abstract class Stat<T> {
    [SerializeField]
    protected T baseValue;
    public T BaseValue { get { return baseValue; } set { baseValue = value; dirty = true; } }

    public delegate void ValueChanged();

    /// <summary>
    /// Called whenever the value is changed.
    /// </summary>
    public event ValueChanged onValueChanged = delegate { };

    /// <summary>
    /// A collection of all the modifiers applied to this stat. Allows duplicates.
    /// </summary>
    protected List<T> modifiers = new List<T>();

    public void AddModifier(T modifier)
    {
        modifiers.Add(modifier);
        dirty = true;
        onValueChanged();
    }

    public bool RemoveModifier(T modifier)
    {
        bool result = modifiers.Remove(modifier);
        if (result)
        {
            //we only need to set dirty if something was actually removed
            dirty = true;
            onValueChanged();
        }
        return result;
    }

    public void Reset() {
        if (modifiers.Count == 0) {
            return;
        }

        modifiers.Clear();
        dirty = true;
    }

    /// <summary>
    /// The value of baseValue, modified by all the modifiers.
    /// </summary>
    protected T actualValue;
    public T Value
    {
        get
        {
            if (dirty)
            {
                actualValue = Recalculate();
                dirty = false;
            }
            return actualValue;
        }
    }
    public static implicit operator T(Stat<T> stat) { return stat.Value; }

    /// <summary>
    /// Dirty flag, for laze evaluation
    /// </summary>
    protected bool dirty = true;

    public Stat(T baseValue)
    {
        this.BaseValue = baseValue;
    }

    /// <summary>
    /// Returns baseValue, with all of the modifiers applied.
    /// </summary>
    protected abstract T Recalculate();
}
