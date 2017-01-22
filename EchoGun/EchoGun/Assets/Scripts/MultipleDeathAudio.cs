using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MultipleDeathAudio : MonoBehaviour, ISpawnable {

    AudioSource sound;

    [SerializeField]
    protected AudioClip[] deathSounds;

    [SerializeField]
    protected int lengthOfArray;

    void ISpawnable.Spawn()
    {
        int randomInt = UnityEngine.Random.Range(0, lengthOfArray);
        sound.PlayOneShot(deathSounds[randomInt]);
        Callback.FireAndForget(() => SimplePool.Despawn(this.transform.root.gameObject), deathSounds[randomInt].length, this);
    }

    // Use this for initialization
    void Awake () {
        sound = GetComponent<AudioSource>();

    }
}
