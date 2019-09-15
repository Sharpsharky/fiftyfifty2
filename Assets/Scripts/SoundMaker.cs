using System.Collections;
using UnityEngine;

[RequireComponent( typeof( AudioSource ) )]
public class SoundMaker : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void PlayShot()
    {
        int index = UnityEngine.Random.Range( 0, audioClips.Length );

        audioSource.PlayOneShot( audioClips[index] );
    }

    internal void StartDelay()
    {
        float secs = UnityEngine.Random.Range( 1.5f, 5f );
        StartCoroutine( PlaySoundAfterDelay( secs ) );
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

    IEnumerator PlaySoundAfterDelay(float secs)
    {
        yield return new WaitForSeconds( secs );

        PlayShot();

        yield return null;
    }
}
