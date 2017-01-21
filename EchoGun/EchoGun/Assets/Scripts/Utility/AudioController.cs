using UnityEngine;
using System.Collections;

//encapsulates things like randomizing sound effects

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour, ISpawnable {

    [SerializeField]
    protected bool PlayOnAwake;

    [SerializeField]
    public AudioAction[] actions;

    [SerializeField]
    protected AudioSource sound;

    public void Spawn()
    {
        if (PlayOnAwake)
            Play();
    }

    public void Play()
    {
        for (int i = 0; i < actions.Length; i++)
            actions[i].ApplyAudioAction(sound);

        sound.Play();
    }
}

public abstract class AudioAction : MonoBehaviour
{
    public abstract void ApplyAudioAction(AudioSource target);
}