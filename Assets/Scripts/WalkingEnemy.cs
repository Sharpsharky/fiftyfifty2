using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour, IObserver<Collider2D>
{

    // private Rigidbody2D rb;
    public GameObject caster;

    public float walkingSpeed = .5f;
    public int waitTime = 2;
    private int isWalking = 1;

    public void Notice(Collider2D t)
    {
       //GetComponent<SpriteRenderer>().color = t.gameObject.GetComponent<SpriteRenderer>().color;
       
       if (isWalking != 0) {
           StartCoroutine(WaitAtPlatform(waitTime));
       }
       
    }
    void Start()
    {
        caster.GetComponent<Caster>().Subscribe(this);
    }


    private void FixedUpdate() {
        Vector3 move = new Vector3(1,0,0);
        Debug.Log("move: " + move);
        Debug.Log("walingSpeed: " + walkingSpeed);
        Debug.Log("stop: " + isWalking);
        transform.position += move * walkingSpeed * Time.deltaTime * isWalking;
    }

    IEnumerator WaitAtPlatform(int secs) {
        isWalking = 0;
        yield return new WaitForSeconds(secs);

        isWalking = 1;
        walkingSpeed *= -1;
        transform.Rotate(0,180,0);
        
    }

}
