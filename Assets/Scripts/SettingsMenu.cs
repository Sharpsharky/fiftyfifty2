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

<<<<<<< Updated upstream
    private void Update() {
=======

    private void Update()
    {
>>>>>>> Stashed changes
        float volume = 0;
        audioMixer.GetFloat( "volume", out volume );
        bool isOn = ( volume < 0 ? true : false );
        audioToggle.isOn = isOn;
<<<<<<< Updated upstream
=======
    }
    void update()
    {
        float volume = 0f;
        audioMixer.GetFloat("volume", out volume);
        bool isOn = (volume < 0 ? true : false);
        audioToggle.isOn = isOn;

>>>>>>> Stashed changes
    }

}
