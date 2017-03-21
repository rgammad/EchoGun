using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Render only a single object to sound buffer, constantly. Place on a gameObject childed to a normal spriteRenderer visuals gameObject
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class ConstantSoundRendering : MonoBehaviour {

    [SerializeField]
    protected float illuminationStrength = 1;

    SpriteRenderer rend;
    SpriteRenderer parent;

    // Use this for initialization
    void Start() {
        rend = GetComponent<SpriteRenderer>();
        parent = transform.parent.GetComponent<SpriteRenderer>();
        int valueID = Shader.PropertyToID("_Value");

        MaterialPropertyBlock block = new MaterialPropertyBlock();
        block.SetFloat(valueID, illuminationStrength);
        rend.SetPropertyBlock(block);

        Assert.IsTrue(transform.localPosition == Vector3.zero);
        Assert.IsTrue(transform.localScale == Vector3.one);
    }

    private void Update() {
        rend.sprite = parent.sprite;
    }
}
