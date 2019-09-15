
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;
using ColdCry.Utility;

public class PlayerGrab : MonoBehaviour, ITriggerListener
{
    bool canHoldTheHair = false;
    public bool isGrabbingHair = false;
    bool canLoseTheHair = false;
    public bool isChangingRotation = false;
    Rigidbody2D rb;
    GameObject hairToGrab = null;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isChangingRotation == true)
        {
            ComeBackToZeroRotation();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "HairBone")
        {
            hairToGrab = col.gameObject;
            Debug.Log("CAN GRAB");
            
            canHoldTheHair = true;
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "HairBone")
        {
            StartCoroutine(MakeHimCantHoldAfterMoments());
        }
    }

    IEnumerator MakeHimCantHoldAfterMoments()
    {
        yield return new WaitForSeconds(0.3f);
        Debug.Log("CAN NOT GRAB");

        canHoldTheHair = false;
        yield return null;
    }

    void GrabTheHair()
    {
        Debug.Log("Grab");
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.parent = hairToGrab.transform.Find("GameObject");
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    void LoseTheHair()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = transform.parent.parent.GetComponent<Rigidbody2D>().velocity * 1.5f;
        transform.parent = null;
        isGrabbingHair = false;
        isChangingRotation = true;
        Debug.Log("Lose");
        StartCoroutine(stopRotating());
    }

    void ComeBackToZeroRotation()
    {
        //float currentRotationZ = transform.rotation.z;

        if(transform.rotation.z >= 180)
        {
            if (transform.rotation.z <= 360)
            {
                transform.Rotate(0,0,5);
            }
        }
        else if(transform.rotation.z < 180 && transform.rotation.z > 0)
        {
            if (transform.rotation.z >= 0)
            {
                transform.Rotate(0,0,-5);

            }
        }
        else if (transform.rotation.z > -180 && transform.rotation.z < 0)
        {
            if (transform.rotation.z <= 0)
            {
                transform.Rotate(0, 0, 5);

            }
        }
        else if (transform.rotation.z <= -180)
        {
            if (transform.rotation.z <= -360)
            {
                transform.Rotate(0, 0, -5);

            }
        }



        /*
        if ((Math.Between(transform.rotation.z, -1, 1) || Math.Between(transform.rotation.z, 359, 361) || Math.Between(transform.rotation.z, -361, -359)))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            isChangingRotation = false;
*/
        
    }

    IEnumerator stopRotating()
    {
        yield return new WaitForSeconds(0.4f);
        transform.eulerAngles = new Vector3(0, 0, 0);
        isChangingRotation = false;
    }

    public void OnTriggerChange(JoystickAxis trigger)
    {
    }

    public void OnTriggerHold(JoystickAxis trigger)
    {

        if (trigger.Code == AxisCode.LeftTrigger)
        {
            if (canHoldTheHair == true && isGrabbingHair == false)
            {
                GrabTheHair();
                isGrabbingHair = true;
                canLoseTheHair = true;
            }
        }
    }

    public void OnTriggerDeadZone(JoystickAxis trigger)
    {
        if (trigger.Code == AxisCode.LeftTrigger)
        {
            if (canLoseTheHair == true)
            {
                LoseTheHair();

                canLoseTheHair = false;
            }
        }
    }
}
