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

    public void playDeath()
    {
        stormtrooperAudio.PlayOneShot(sounds[Random.Range(0, 2)]);
    }

    public void playFiringLaser()
    {
        if (!stormtrooperAudio.isPlaying)
            stormtrooperAudio.PlayOneShot(sounds[Random.Range(3,5)]);
    }

    public void playJobsDone()
    {
        stormtrooperAudio.PlayOneShot(sounds[6]);
    }

    public void playBlasterSound()
    {
        stormtrooperAudio.PlayOneShot(sounds[7],0.1f);
    }
}
