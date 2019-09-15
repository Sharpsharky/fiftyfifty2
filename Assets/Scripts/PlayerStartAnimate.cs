using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;
public class PlayerStartAnimate : MonoBehaviour
{

    float xPosStop = 0;
    Rigidbody2D rb;
    Animator animator;
    bool setIdleForOnce = false;
    EarthGoDown earth;

    PlayerMove pm;
    PlayerGrab pg;
    PInput pi;



    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMove>();
        pg = GetComponent<PlayerGrab>();
        pi = GetComponent<PInput>();

        pm.enabled = false;
        pg.enabled = false;
        pi.enabled = false;

        earth = GameObject.Find( "Earth" ).GetComponent<EarthGoDown>();

        Debug.Log("Animate");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(-0.3f, 0.3f, 1);
        rb.velocity = new Vector2(5, 0);
        animator.SetTrigger("Walk");


    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= xPosStop)
        {
            rb.velocity = new Vector2(0, 0);
            if(setIdleForOnce == false)
            {
                setIdleForOnce = true;
                animator.SetTrigger("Idle");
                StartCoroutine(WaitAMomentForStart());
            }

        }
        else
        {
            rb.velocity = new Vector2(10, 0);

        }
    }

    IEnumerator WaitAMomentForStart()
    {
        yield return new WaitForSeconds(2f);

        CallStartGame();
        yield return null;
    }


    void CallStartGame()
    {
        pm.enabled = true;
        pg.enabled = true;
        pi.enabled = true;


        earth.isGoingDown = true;
        earth.startedcoroutine = true;

        //Tutaj start z klasy Marcina
       GameManager.StartGame();


        this.enabled = false;
    }

}
