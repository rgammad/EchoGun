using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSliders : MonoBehaviour {

    public Slider musicSlider;
    public Slider masterSlider;

    private AudioSource musicVolume;

    void Awake()
    {
        GameObject musicManager = MusicManager.instance.gameObject;
        musicVolume = musicManager.GetComponent<AudioSource>();

        musicSlider.value = musicVolume.volume;
        masterSlider.value = AudioListener.volume;
    }
    public void OnMusicSliderChange()
    {
        musicVolume.volume = musicSlider.value;
        PlayerPrefs.SetFloat("Music Volume", musicVolume.volume);
        PlayerPrefs.Save();
    }

    public void OnMasterSliderChange()
    {
        AudioListener.volume = masterSlider.value;
        PlayerPrefs.SetFloat("Master Volume", AudioListener.volume);
        PlayerPrefs.Save();
    }

}
