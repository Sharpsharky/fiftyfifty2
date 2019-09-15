using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Play() {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1); // Load the next scene
    }

    public void Quit() {
        Application.Quit(); // Well... Quit the application
    }

}
