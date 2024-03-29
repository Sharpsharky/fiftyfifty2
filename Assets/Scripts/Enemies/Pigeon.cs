﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Pigeon : FlyingEnemy
{
    public SpriteRenderer head;
    public Transform bodyJoint;
    public Transform backWingJoint;
    public Transform frontWingJoint;

    private SoundMaker soundMaker;

    public override void Awake()
    {
        base.Awake();
        head = transform.Find( "HeadPart" ).GetComponent<SpriteRenderer>();
        soundMaker = GetComponent<SoundMaker>();
    }

    public void FixedUpdate()
    {
        if ( ShouldDisapear() ) {
            PigeonFactory.ReturnPigeon( this );
        }
    }

    public override void StartMoving()
    {
        rb.velocity = new Vector2( Direction, 0 ) * MoveSpeed;
        soundMaker.StartDelay();
    }

    public override void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }

    public GameObject GetBodyPart()
    {
        return bodyJoint.GetChild( 0 ).gameObject;
    }

    public GameObject GetBackWingPart()
    {
        return backWingJoint.GetChild( 0 ).gameObject;
    }

    public GameObject GetFrontWingPart()
    {
        return frontWingJoint.GetChild( 0 ).gameObject;
    }


}
