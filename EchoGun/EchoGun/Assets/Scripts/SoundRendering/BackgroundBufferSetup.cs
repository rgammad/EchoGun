using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class BackgroundBufferSetup : MonoBehaviour {

    protected RenderTexture screenBackgroundTexture; //texture to represent what areas are illuminated by sound
    public RenderTexture ScreenBackgroundTexture { get { return screenBackgroundTexture; } }

    Camera targetCamera;

    // Use this for initialization
    void Awake() {
        screenBackgroundTexture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.Default);
        targetCamera = GetComponent<Camera>();
        targetCamera.targetTexture = screenBackgroundTexture;
    }
}
