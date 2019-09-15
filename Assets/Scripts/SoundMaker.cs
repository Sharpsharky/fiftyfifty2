using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundMaker : MonoBehaviour
{

    public AudioClip[] audioClips;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        int secs = Random.Range(1, 5);
        StartCoroutine(PlaySoundAfterDelay(secs));
    }

    void PlayShot()
    {
        int index = Random.Range(0, audioClips.Length);

        audioSource.PlayOneShot(audioClips[index]);
    }

    // Update is called once per frame
    // void Update()
    // {
    //     // if (Input.GetKeyDown(KeyCode.F))
    //     // {
    //     //     ///Debug.Log("Key down!");
    //     //     PlayShot();
    //     // }
    // }

    IEnumerator PlaySoundAfterDelay(int secs) 
    {
        yield return new WaitForSeconds(secs);

        PlayShot();

        yield return null;
    }
}
