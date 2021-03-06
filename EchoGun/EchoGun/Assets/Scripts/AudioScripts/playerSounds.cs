﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    AudioSource playerAudio;
    public AudioClip[] sounds;
	// Use this for initialization
	void Start () {
        playerAudio = GetComponent<AudioSource>();
	}
	//0 footstep
    //1 Dash
    //2 Player Death
    //3 LaserFire
    //4 PlayerHitscanFire
	// Update is called once per frame
	void Update () {
		
	}

    public void playFootstep()
    {
        System.Random r = new System.Random();
        
        //playerAudio.pitch = (float)r.NextDouble() * 0.5f;
        playerAudio.PlayOneShot(sounds[0]);   
    }

    public void playDash()
    {
        playerAudio.PlayOneShot(sounds[1],0.20f);
    }

    public void playDeath()
    {
        playerAudio.PlayOneShot(sounds[2]);
    }

    public void playLaserSound()
    {
        playerAudio.PlayOneShot(sounds[3],0.4f);
    }

    public void playHitscan()
    {
        playerAudio.PlayOneShot(sounds[4],0.25f);
    }

    public void playBonk()
    {
        playerAudio.PlayOneShot(sounds[5]);
    }

    public void playDamage()
    {
        playerAudio.PlayOneShot(sounds[6]);
    }

    public void playWallImpact()
    {
        playerAudio.PlayOneShot(sounds[7], 0.1f);
    }

    public void playFleshImpact()
    {
        playerAudio.PlayOneShot(sounds[8], 0.1f);
    }
}
