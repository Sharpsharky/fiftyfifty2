using UnityEngine;

[RequireComponent( typeof( AudioSource ) )]
public class PlayerAudio : MonoBehaviour
{
    public AudioClip[] jumpClips;
    public AudioClip dead;
    public AudioClip walking;

    private AudioSource audioSource;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayJumpShot()
    {
        int index = UnityEngine.Random.Range( 0, jumpClips.Length );
        audioSource.PlayOneShot( jumpClips[index] );
    }

    public void PlayDeadShot()
    {
        audioSource.PlayOneShot(dead);
    }

}
