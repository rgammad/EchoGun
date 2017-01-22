using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerSounds : MonoBehaviour {

    AudioSource rollerAudio;
    public AudioClip[] sounds;

	// Use this for initialization
	void Start () {
        rollerAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playExplosion()
    {
        rollerAudio.PlayOneShot(sounds[0]);
    }
    
    public void playBonk()
    {
        rollerAudio.PlayOneShot(sounds[1]);
    }

    public void playRollingSound()
    {
        if (!rollerAudio.isPlaying)
        {
            rollerAudio.PlayOneShot(sounds[2]);
        }
    }
}
