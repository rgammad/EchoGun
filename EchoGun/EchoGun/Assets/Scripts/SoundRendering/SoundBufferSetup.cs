using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SoundBufferSetup : MonoBehaviour {

    RenderTexture screenSoundTexture; //texture to represent what areas are illuminated by sound
    Camera targetCamera;

	// Use this for initialization
	void Awake () {
        screenSoundTexture = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.Default);
        targetCamera = GetComponent<Camera>();
        targetCamera.targetTexture = screenSoundTexture;
        screenSoundTexture.SetGlobalShaderProperty("_GlobalRenderedSoundTex");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
