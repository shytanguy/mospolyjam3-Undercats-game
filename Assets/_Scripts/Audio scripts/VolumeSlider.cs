using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class VolumeSlider : MonoBehaviour
{
    private Slider slider;

    [SerializeField] private bool MusicVolume;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        if (MusicVolume)
            slider.value = PlayerPrefs.GetFloat("music volume", 0.5f);
        else
            slider.value = PlayerPrefs.GetFloat("sfx volume", 0.5f);
    }


   
    public void ChangeVolumeMusic()
    {

        AudioManager.audioManager.ChangeVolumeMusic(slider.value);

        AudioManager.audioManager.SaveVolume();
    }

   
    public void ChangeVolumeSound()
    {
        AudioManager.audioManager.ChangeVolumeSfx(slider.value);

        AudioManager.audioManager.SaveVolume();
    }
}
