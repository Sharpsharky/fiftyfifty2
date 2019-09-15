using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;

    public void ToggleAudio(bool isChecked){

        int newVolume = -80;
        if (isChecked) {
            newVolume = 0;
        }

        audioMixer.SetFloat("volume", newVolume);
    }

    public void SetQuality(int index) {
        QualitySettings.SetQualityLevel(index);
    }

}
