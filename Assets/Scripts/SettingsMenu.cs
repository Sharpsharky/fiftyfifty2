using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Toggle audioToggle;

    public void ToggleAudio(bool isChecked){

        int newVolume = -80;
        if (!isChecked) {
            newVolume = 0;
        }

        audioMixer.SetFloat("volume", newVolume);
    }

    public void SetQuality(int index) {
        QualitySettings.SetQualityLevel(index);
    }

    private void Update() {
        float volume = 0;
        audioMixer.GetFloat("volume", out volume);
        bool isOn = (volume < 0 ? true : false);
        audioToggle.isOn = isOn;
    }

}
