<<<<<<< Updated upstream
﻿using Inputs;
=======
using Inputs;
>>>>>>> Stashed changes
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour, IButtonListener
{
    private bool isPaused = false;

    private void Update()
    {

        if (Input.GetKeyDown( KeyCode.Escape )) {
            Debug.Log( "Escape pressed!" );
            isPaused = !isPaused;
        }

        this.gameObject.transform.GetChild( 0 ).gameObject.SetActive( isPaused );
        Time.timeScale = ( isPaused ? 0 : 1 );
    }


    public void Quit()
    {
<<<<<<< Updated upstream
        SceneManager.LoadScene(0);
=======
        SceneManager.LoadSceneAsync( SceneManager.GetActiveScene().buildIndex - 1 );
>>>>>>> Stashed changes
    }

    public void OnButtonPressed(ButtonCode code)
    {
<<<<<<< Updated upstream
=======

        if (code == ButtonCode.Start) {
            isPaused = !isPaused;
        }
>>>>>>> Stashed changes
        if (code == ButtonCode.Start) {
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
