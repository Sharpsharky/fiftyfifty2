using DoubleMMPrjc.Timer;
using UnityEngine;

public class Shit : MonoBehaviour
{
    private float growSpeed = 0.13f;
    private bool grow = false;

    public float GrowSpeed { get => growSpeed; set => growSpeed = value; }
    public bool Grow { get => grow; set => grow = value; }

    public void ReleaseTheOrca()
    {
        transform.localScale = Vector3.zero;
        grow = true;
    }

    public void Update()
    {
        if (grow) {
            transform.localScale = transform.localScale + new Vector3(1f, 8f, 1f) * growSpeed * Time.deltaTime;
            transform.position = transform.position - new Vector3( 0, 3.3f, 0 ) * Time.deltaTime;
            if (transform.localScale.x > 0.25f) {
                grow = false;
              //  PigeonFactory.ReturnShit( this );
                return;
            }
        }
    }

}
