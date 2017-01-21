using UnityEngine;
using System.Collections;

public class RandomizePitchAudioAction : AudioAction {
    [SerializeField]
    protected float minPitch;

    [SerializeField]
    protected float maxPitch;

    public override void ApplyAudioAction(AudioSource target)
    {
        target.pitch = Random.Range(minPitch, maxPitch);
    }
}
