using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAfterDelay());
    }


    IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds( 3 );

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
