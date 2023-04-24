using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundOption : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider MasterSlider;
    public Slider BgmSlider;
    public Slider SfxSlider;

    void Start()
    {
        
    }

    public void SetMasterVolume()
    {
        if(!(MasterSlider.value == 0))
        {
            audioMixer.SetFloat("Master", Mathf.Log10(MasterSlider.value) * 20);
        } else
        {
            audioMixer.SetFloat("Master", -80.0f);
        }
    }

    public void SetBGMVolume()
    {
        if(!(BgmSlider.value == 0))
        {
            audioMixer.SetFloat("BGM", Mathf.Log10(BgmSlider.value) * 20);
        } else
        {
            audioMixer.SetFloat("BGM", -80.0f);
        }
    }

    public void SetSFXVolume()
    {
        if(!(SfxSlider.value == 0))
        {
            audioMixer.SetFloat("SFX", Mathf.Log10(SfxSlider.value) * 20);
        } else
        {
            audioMixer.SetFloat("SFX", -80.0f);
        }       
    }
}
