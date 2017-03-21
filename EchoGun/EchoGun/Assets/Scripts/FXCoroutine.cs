using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public interface FXComponent {
    void TriggerEffect();
}

/// <summary>
/// Abstract class for coroutine-based effects. Implementing classes should override OnEffectBegin, OnEffectUpdate, OnEffectComplete, or OnEffectInterrupted
/// </summary>
public abstract class FXCoroutine : MonoBehaviour, FXComponent {

    [SerializeField]
    protected float duration = 1;

    Coroutine coroutine = null; //the active coroutine controlling the effect. Can be null, if effect is inactive

    public void TriggerEffect() {

        if (coroutine != null) {
            OnEffectInterrupted();
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(EffectRoutine());
    }

    IEnumerator EffectRoutine() {
        OnEffectBegin();

        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time <= endTime) {
            float lerpValue = Mathf.InverseLerp(startTime, endTime, Time.time);
            OnEffectUpdate(lerpValue);
            yield return null;
        }

        //coroutine complete
        OnEffectComplete();

        coroutine = null;
    }

    protected virtual void OnEffectBegin() {}

    protected virtual void OnEffectUpdate(float lerpValue) {}

    protected virtual void OnEffectComplete() {}

    protected virtual void OnEffectInterrupted() {}
}
