using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class SoundBufferSetup : MonoBehaviour {

    protected RenderTexture screenSoundTexture; //texture to represent what areas are illuminated by sound
    public RenderTexture ScreenSoundTexture { get { return screenSoundTexture; } }

    Camera targetCamera;

	// Use this for initialization
	void Awake () {
        screenSoundTexture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.Default);
        targetCamera = GetComponent<Camera>();
        targetCamera.targetTexture = screenSoundTexture;
    }
}
