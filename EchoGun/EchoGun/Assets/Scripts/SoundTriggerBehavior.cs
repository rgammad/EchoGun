﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(CircleCollider2D))]
public class SoundTriggerBehavior : MonoBehaviour
{

    /// <summary>
    /// Represents a section of an animation. The duration of animation phases can be changed without affecting other phases.
    /// </summary>
    [System.Serializable]
    public class AnimationPhase {
        [SerializeField]
        protected AnimationCurve valueOverTime = AnimationCurve.Linear(0, 0, 1, 1);
        public AnimationCurve ValueOverTime { get { return valueOverTime; } }

        [SerializeField]
        protected float duration = 1;
        public float Duration { get { return duration; } }

        /// <summary>
        /// Basic Constructor.
        /// </summary>
        /// <param name="valueOverTime"></param>
        /// <param name="duration"></param>
        public AnimationPhase(AnimationCurve valueOverTime, float duration) {
            this.valueOverTime = valueOverTime;
            this.duration = duration;
        }
    }

    [SerializeField]
    protected AnimationPhase[] phases = new AnimationPhase[1] { new AnimationPhase(AnimationCurve.Linear(0, 0, 1, 1), 1) };

    [SerializeField]
    protected float maxValue = 1;

    float phaseStartTime;
    float phaseEndTime;
    int phaseIndex = 0;

    protected AnimationPhase currentPhase { get { return phases[phaseIndex]; } }

    private CircleCollider2D soundTrigger;

    protected virtual void Start() {
        if (phases.Length == 0) {
            Debug.LogWarning("The script does not have any Animation Phases, destroying self", this);
            Destroy(this);
            return;
        }
        phaseStartTime = Time.time;
        phaseEndTime = phaseStartTime + currentPhase.Duration;

        soundTrigger = gameObject.GetComponent<CircleCollider2D>();
        soundTrigger.radius = 0;
    }

    protected void Update() {
        while (Time.time >= phaseEndTime) {
            //move to next phase
            phaseIndex++;
            if (phaseIndex >= phases.Length) {
                //all phases complete
                Destroy(this);
                return;
            }

            phaseStartTime = phaseEndTime; //current phase starts when the previous phase ends
            phaseEndTime = phaseStartTime + currentPhase.Duration;
        }

        float lifetimeProgress = Mathf.InverseLerp(phaseStartTime, phaseEndTime, Time.time);
        float value = currentPhase.ValueOverTime.Evaluate(lifetimeProgress) * maxValue;
        Evaluate(value);
    }

    protected void Evaluate(float currentValue) {
        soundTrigger.radius = currentValue;
    }

    protected void OnDestroy() {
        Destroy(soundTrigger);
    }
}
