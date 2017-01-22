using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSounds : MonoBehaviour {

    AudioSource playerAudio;
    public AudioClip[] sounds;
	// Use this for initialization
	void Start () {
        playerAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
