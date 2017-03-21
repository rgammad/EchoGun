using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRenderingBehaviour : MonoBehaviour, ISpawnable
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
        protected AnimationCurve edgeStrOverTime = AnimationCurve.Linear(0, 0, 1, 1);
        public AnimationCurve EdgeStrOverTime { get { return edgeStrOverTime; } }
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
    MeshRenderer meshRenderer;

    [SerializeField]
    protected AnimationPhase[] phases = new AnimationPhase[1] { new AnimationPhase(AnimationCurve.Linear(0, 0, 1, 1), 1) };

    [SerializeField]
    protected float maxValue = 1;

    int edgeStrID;
    int illumStrID;

    float phaseStartTime;
    float phaseEndTime;
    int phaseIndex;

    protected AnimationPhase currentPhase { get { return phases[phaseIndex]; } }

    protected void Awake() {
        meshRenderer = rendering.GetComponent<MeshRenderer>();

        edgeStrID = Shader.PropertyToID("_EdgeStrength");
        illumStrID = Shader.PropertyToID("_IllumStrength");
    }

    void ISpawnable.Spawn() {
        this.enabled = true;
        meshRenderer.enabled = true;

        phaseIndex = 0;
        
        if (phases.Length == 0) {
            Debug.LogWarning("The script does not have any Animation Phases, disabling self", this);
            this.enabled = false;
            meshRenderer.enabled = false;
            return;
        }
        phaseStartTime = Time.time;
        phaseEndTime = phaseStartTime + currentPhase.Duration;
        meshRenderer.sortingOrder = (Mathf.RoundToInt(10 * Time.time) % 32760);
        //max value is 32767
        //newer ones take precedence over older ones
    }

    protected void Update() {
        while (Time.time >= phaseEndTime) {
            //move to next phase
            phaseIndex++;
            if (phaseIndex >= phases.Length) {
                //all phases complete
                this.enabled = false;
                meshRenderer.enabled = false;
                return;
            }

            phaseStartTime = phaseEndTime; //current phase starts when the previous phase ends
            phaseEndTime = phaseStartTime + currentPhase.Duration;
        }

        float lifetimeProgress = Mathf.InverseLerp(phaseStartTime, phaseEndTime, Time.time);

        float size = currentPhase.SizeOverTime.Evaluate(lifetimeProgress) * maxValue;
        float alphaStr = currentPhase.EdgeStrOverTime.Evaluate(lifetimeProgress);
        float illumStr = currentPhase.IllumStrOverTime.Evaluate(lifetimeProgress);

        Evaluate(size, alphaStr, illumStr);
    }

    protected void Evaluate(float size, float edgeStr, float illumStr) {
        rendering.transform.localScale = 2 * size * Vector3.one;


        MaterialPropertyBlock block = new MaterialPropertyBlock();

        block.SetFloat(edgeStrID, edgeStr);
        block.SetFloat(illumStrID, illumStr);

        meshRenderer.SetPropertyBlock(block);
    }
}
