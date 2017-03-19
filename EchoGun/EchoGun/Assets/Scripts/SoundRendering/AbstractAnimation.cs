using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An abstract base class for any gameplay logic which accesses an animationCurve-controlled value.
/// 
/// Abstract methods: Evaluate(float), called every frame with the current animation value.
/// </summary>
public abstract class AbstractAnimation : MonoBehaviour {

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

    protected virtual void Start() {
        if(phases.Length == 0) {
            Debug.LogWarning("The script does not have any Animation Phases, destroying self", this);
            Destroy(this);
            return;
        }
        phaseStartTime = Time.time;
        phaseEndTime = phaseStartTime + currentPhase.Duration;
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

    /// <summary>
    /// Called every frame, with the current scaled value of the animation.
    /// </summary>
    /// <param name="currentValue"></param>
    protected abstract void Evaluate(float currentValue);
}

