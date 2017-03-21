using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Initializes the MaterialPropertyBlock for a constant small "spotlight" of sound. Size is controlled by renderer size.
/// </summary>
[ExecuteInEditMode]
public class SoundRenderingSpotlight : MonoBehaviour {

    [SerializeField]
    [Range(0, 1)]
    protected float illuminationStrength = 1;

    [SerializeField]
    [Range(0, 1)]
    protected float edgeStrength = 1;

    void Start () {
        int edgeStrID = Shader.PropertyToID("_EdgeStrength");
        int illumStrID = Shader.PropertyToID("_IllumStrength");

        MaterialPropertyBlock block = new MaterialPropertyBlock();

        block.SetFloat(edgeStrID, edgeStrength);
        block.SetFloat(illumStrID, illuminationStrength);

        GetComponentInChildren<Renderer>().SetPropertyBlock(block);
    }
}
