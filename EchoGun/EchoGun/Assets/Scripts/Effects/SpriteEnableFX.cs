using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteEnableFX : FXCoroutine {

    SpriteRenderer rend;

	void Start () {
        rend = GetComponent<SpriteRenderer>();
        rend.enabled = false;
	}

    protected override void OnEffectBegin() {
        base.OnEffectBegin();
        rend.enabled = true;
    }

    protected override void OnEffectComplete() {
        base.OnEffectComplete();
        rend.enabled = false;
    }
}
