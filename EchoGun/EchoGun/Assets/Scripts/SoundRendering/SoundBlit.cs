using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class SoundBlit : MonoBehaviour {

    [SerializeField]
    protected Material mat;

    [SerializeField]
    protected SoundBufferSetup soundBuffer;
    [SerializeField]
    protected BackgroundBufferSetup backgroundBuffer;

    private void Start() {
        mat.SetTexture("_RenderedSoundTex", soundBuffer.ScreenSoundTexture);
        mat.SetTexture("_RenderedBackgroundTex", backgroundBuffer.ScreenBackgroundTexture);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
        Graphics.Blit(source, destination, mat);
    }
}
