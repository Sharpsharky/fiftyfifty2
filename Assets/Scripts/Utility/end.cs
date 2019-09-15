using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class end : MonoBehaviour
{

    VideoPlayer d;
    public void Awake()
    {
       d = GameObject.FindObjectOfType<VideoPlayer>();


    }

    IEnumerator wait(float secs)
    {
        yield return new WaitForSeconds( 4f );
        if ( !d.isPlaying ) {
            NextScene();
        }
        yield return new WaitForSeconds( secs );
    }

    public void Update()
    {
        StartCoroutine(wait(1f));
    }

    public void NextScene()
    {
        SceneManager.LoadScene( 0 );
    }

}
