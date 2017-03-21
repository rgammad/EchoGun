using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Render only a single object to sound buffer. Place on a gameObject childed to a normal spriteRenderer visuals gameObject
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class ObjectSoundVisuals : FXCoroutine {

    [SerializeField]
    protected AnimationCurve intensityOverTime = AnimationCurve.Linear(0, 1, 1, 0);

    SpriteRenderer rend;
    SpriteRenderer parent;

    int valueID;

    // Use this for initialization
    void Start () {
        rend = GetComponent<SpriteRenderer>();
        parent = transform.parent.GetComponent<SpriteRenderer>();
        rend.enabled = false;
        valueID = Shader.PropertyToID("_Value");

        Assert.IsTrue(transform.localPosition == Vector3.zero);
        Assert.IsTrue(transform.localScale == Vector3.one);
	}

    protected override void OnEffectBegin() {
        base.OnEffectBegin();
        rend.enabled = true;
    }

    protected override void OnEffectUpdate(float lerpValue) {
        rend.sprite = parent.sprite;

        float value = intensityOverTime.Evaluate(lerpValue);
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        block.SetFloat(valueID, value);
        rend.SetPropertyBlock(block);
    }

    protected override void OnEffectComplete() {
        base.OnEffectComplete();
        rend.enabled = false;
    }
}
