using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource music;
    public static MusicManager instance;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else {
            instance = this;
            music = GetComponent<AudioSource>();
        }
        DontDestroyOnLoad(this.gameObject);
        music.Play();
    }
}
