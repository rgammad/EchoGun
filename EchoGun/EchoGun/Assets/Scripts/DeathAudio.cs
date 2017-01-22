using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DeathAudio : MonoBehaviour, ISpawnable {

    AudioSource sound;
    ParticleSystem particles;

    [SerializeField]
    protected AudioClip deathSound;

    void ISpawnable.Spawn()
    {
        sound.PlayOneShot(deathSound);
        particles.Play();
        Callback.FireAndForget(() => SimplePool.Despawn(this.transform.root.gameObject), deathSound.length, this);
    }

    // Use this for initialization
    void Awake () {
        sound = GetComponent<AudioSource>();
        particles = GetComponent<ParticleSystem>();
    }
}
