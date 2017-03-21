using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Camera))]
public class SoundBlit : MonoBehaviour {

    [SerializeField]
    protected Material mat;

    [SerializeField]
    protected SoundBufferSetup soundBuffer;

    private void Start() {
        mat.SetTexture("_RenderedSoundTex", soundBuffer.ScreenSoundTexture);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
        Graphics.Blit(source, destination, mat);
    }
}
