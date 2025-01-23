using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SFXControl : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider SFXSlider;

    private void Awake()
    {
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    // Update is called once per frame
    void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            SFXSlider.value = PlayerPrefs.GetFloat("Volume");
        }

        else

            SFXSlider.value = 0.5f;
            audioMixer.SetFloat("SFX", Mathf.Log10(SFXSlider.value) * 20);

    }

    public void SetSFXVolume(float volume)
    {
        SoundManager.instance.sfxPlayer.volume = volume;
        //audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Volume", SFXSlider.value);
    }
}
