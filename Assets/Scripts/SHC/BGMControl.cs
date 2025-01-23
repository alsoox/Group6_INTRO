using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class BGMControl : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider BGMSlider;
    
    

    private void Awake()
    {
        BGMSlider.onValueChanged.AddListener(SetBGMVolume);
    }

    // Update is called once per frame
    void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            BGMSlider.value = PlayerPrefs.GetFloat("Volume");
            
        }
        else
        
            BGMSlider.value = 0.5f;
            audioMixer.SetFloat("BGM", Mathf.Log10(BGMSlider.value) * 20);
            
        

    }
    public void SetBGMVolume(float volume)
    {
        SoundManager.instance.bgmPlayer.volume = volume;
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Volume", BGMSlider.value);
        
    }

   
    
    
}
