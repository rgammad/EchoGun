using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormtrooperSound : MonoBehaviour {

    AudioSource stormtrooperAudio;
    public AudioClip[] sounds;
	// Use this for initialization
	void Start () {
        stormtrooperAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
