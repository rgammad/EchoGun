using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRenderingBehaviour : MonoBehaviour
{
    /// <summary>
    /// Represents a section of an animation. The duration of animation phases can be changed without affecting other phases.
    /// </summary>
    [System.Serializable]
    public class AnimationPhase {
        [SerializeField]
        protected AnimationCurve sizeOverTime = AnimationCurve.Linear(0, 0, 1, 1);
        public AnimationCurve SizeOverTime { get { return sizeOverTime; } }

        [SerializeField]
        protected AnimationCurve alphaStrOverTime = AnimationCurve.Linear(0, 0, 1, 1);
        public AnimationCurve AlphaStrOverTime { get { return alphaStrOverTime; } }
        //Scalar strength of the high-alpha edge

        [SerializeField]
        protected AnimationCurve illumStrOverTime = AnimationCurve.Linear(0, 0, 1, 1);
        public AnimationCurve IllumStrOverTime { get { return illumStrOverTime; } }
        //Scalar strength of the overall output

        [SerializeField]
        protected float duration = 1;
        public float Duration { get { return duration; } }

        /// <summary>
        /// Basic Constructor.
        /// </summary>
        /// <param name="valueOverTime"></param>
        /// <param name="duration"></param>
        public AnimationPhase(AnimationCurve valueOverTime, float duration) {
            this.sizeOverTime = valueOverTime;
            this.duration = duration;
        }
    }

    [SerializeField]
    protected Transform rendering; //the gameobject holding the quad which will be rendered into the sound buffer

    [SerializeField]
    protected AnimationPhase[] phases = new AnimationPhase[1] { new AnimationPhase(AnimationCurve.Linear(0, 0, 1, 1), 1) };

    [SerializeField]
    protected float maxValue = 1;

    float phaseStartTime;
    float phaseEndTime;
    int phaseIndex = 0;

    protected AnimationPhase currentPhase { get { return phases[phaseIndex]; } }

    protected void Start() {
        if (phases.Length == 0) {
            Debug.LogWarning("The script does not have any Animation Phases, destroying self", this);
            Destroy(this);
            return;
        }
        phaseStartTime = Time.time;
        phaseEndTime = phaseStartTime + currentPhase.Duration;

        rendering.GetComponent<MeshRenderer>().sortingOrder = (Mathf.RoundToInt(Time.time) % 32760);
        //max value is 32767
        //newer ones take precedence over older ones
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
        float value = currentPhase.SizeOverTime.Evaluate(lifetimeProgress) * maxValue;
        Evaluate(value);
    }

    protected void Evaluate(float currentValue) {
        rendering.transform.localScale = 2 * currentValue * Vector3.one;
    }
}
