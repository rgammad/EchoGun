using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(AudioSource))]
public class PlayAudioClip : MonoBehaviour, FXComponent {

    [SerializeField]
    protected AudioClip clip;

    [SerializeField]
    protected float volumeScale = 1;

    AudioSource source;

    void Awake() {
        source = GetComponent<AudioSource>();
    }

    void FXComponent.TriggerEffect() {
        source.PlayOneShot(clip, volumeScale);
    }
}
