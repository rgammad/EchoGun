using UnityEngine;
using System.Collections;

public class RandomizeClipAudioAction : AudioAction {
    [SerializeField]
    protected AudioClip[] clips;
    public override void ApplyAudioAction(AudioSource target)
    {
        target.clip = clips[Random.Range(0, clips.Length)];
    }
}
