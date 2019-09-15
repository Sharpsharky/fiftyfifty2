using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Inputs;

public class PauseMenu : MonoBehaviour, IButtonListener
{
    private bool isPaused = false; 
    private void Update() {
        
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("Escape pressed!");
            isPaused = !isPaused;
        } 

        this.gameObject.transform.GetChild(0).gameObject.SetActive(isPaused);
        Time.timeScale = (isPaused ? 0 : 1);
    }


    public void Quit() {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void OnButtonPressed(ButtonCode code)
    {
        if (code == ButtonCode.Start)
        {
            isPaused = !isPaused;   
        }
    }

    public void OnButtonReleased(ButtonCode code)
    {
    }

    public void OnButtonHeld(ButtonCode code)
    {
    }
}
