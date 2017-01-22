using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperSound : MonoBehaviour {

    AudioSource sniperAudio;
    public AudioClip[] sounds;

    bool wasPlayed = false;
    // Use this for initialization
    void Start()
    {
        sniperAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playDeath()
    {
        sniperAudio.PlayOneShot(sounds[0]);
    }

    public void playAttackMissed()
    {
        sniperAudio.PlayOneShot(sounds[1]);
    }

    public void playKillConfirmed()
    {
        sniperAudio.PlayOneShot(sounds[2]);
    }

    public void playLostVisual()
    {
        sniperAudio.PlayOneShot(sounds[3]);
    }

    public void playWhatWasThat()
    {
        if (!sniperAudio.isPlaying)
            sniperAudio.PlayOneShot(sounds[4]);
    }

    public void playDamaged()
    {
        sniperAudio.PlayOneShot(sounds[Random.Range(5,6)]);
    }

    public void playAiming()
    {
        sniperAudio.PlayOneShot(sounds[7]);
    }

    public void playSniperFire()
    {
        sniperAudio.PlayOneShot(sounds[8]);
    }

    public void toggleWasPlayed(bool toset)
    {
        wasPlayed = toset;
    }

    public bool WasPlayed()
    {
        return wasPlayed;
    }
}
